using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using OneForAll.Core.Upload;
using OneForAll.EFCore;
using OneForAll.File;
using OneForAll.File.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.OAuth;
using Pms.Domain.Aggregates;
using AutoMapper.Execution;

namespace Pms.Domain
{
    /// <summary>
    /// 项目任务
    /// </summary>
    public class PmsTaskManager : PmsBaseManager, IPmsTaskManager
    {
        // 文件存储路径
        private readonly string UPLOAD_PATH = "upload/projects/{0}/tasks/{1}/";
        // 虚拟路径：根据Startup配置设置,返回给前端访问资源
        private readonly string VIRTUAL_PATH = "resources/projects/{0}/tasks/{1}/";

        private readonly IUploader _uploader;
        private readonly IPmsTaskRepository _repository;
        private readonly IPmsTaskFileRepository _fileRepository;
        private readonly IPmsRequirementRepository _requirementReposiotry;
        private readonly IPmsTaskMemberContactRepository _memberContactRepository;
        private readonly IPmsProjectMemberContactRepository _projectMemberContactRepository;

        public PmsTaskManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUploader uploader,
            IPmsTaskRepository repository,
            IPmsTaskFileRepository fileRepository,
            IPmsRequirementRepository requirementReposiotry,
            IPmsTaskMemberContactRepository memberContactRepository,
            IPmsProjectMemberContactRepository projectMemberContactRepository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _repository = repository;
            _fileRepository = fileRepository;
            _requirementReposiotry = requirementReposiotry;
            _memberContactRepository = memberContactRepository;
            _projectMemberContactRepository = projectMemberContactRepository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>任务分页</returns>
        public async Task<PageList<PmsTaskAggregate>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            var result = new PageList<PmsTaskAggregate>(0, pageIndex, pageSize, new List<PmsTaskAggregate>());

            var data = await _repository.GetPageAsync(projectId, pageIndex, pageSize, key);
            var ids = data.Items.Select(s => s.Id).ToList();

            var files = await _fileRepository.GetListByTaskAsync(ids);
            var members = await _memberContactRepository.GetListByTaskAsync(ids);

            var items = data.Items.Select(task => new PmsTaskAggregate()
            {
                Task = task,
                Files = files.Where(w => w.PmsTaskId == task.Id).ToList(),
                MemberContacts = members.Where(w => w.Contact.PmsTaskId == task.Id).ToList()
            }).ToList();

            return new PageList<PmsTaskAggregate>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsTaskForm form)
        {
            var requirement = await _requirementReposiotry.FindAsync(form.RequirementId);
            if (requirement == null)
                return BaseErrType.DataError;
            if (form.UserIds.Count < 1)
                return BaseErrType.DataEmpty;
            if (requirement.PmsProjectId != projectId)
                return BaseErrType.DataError;

            var members = await _projectMemberContactRepository.GetListByProjectAsync(projectId);
            members = members.Where(w => form.UserIds.Contains(w.SysUserId)).ToList();
            var files = _mapper.Map<IEnumerable<PmsTaskFileForm>, IEnumerable<PmsTaskFile>>(form.Files);

            var data = _mapper.Map<PmsTaskForm, PmsTask>(form);
            data.PmsRequirementId = requirement.Id;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            data.PmsProjectId = projectId;
            data.UpdateTime = DateTime.Now;

            var aggregate = new PmsTaskAggregate() { Task = data };
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.AddAsync(data, tran);

                aggregate.AssignSubTasks(members);
                aggregate.AddFiles(files);
                var contacts = aggregate.MemberContacts.Select(s => s.Contact).ToList();

                await _memberContactRepository.AddRangeAsync(contacts, tran);
                await _fileRepository.AddRangeAsync(files, tran);

                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsTaskForm form)
        {
            var requirement = await _requirementReposiotry.FindAsync(form.RequirementId);
            if (requirement == null)
                return BaseErrType.DataNotFound;
            if (form.UserIds.Count < 1)
                return BaseErrType.DataEmpty;
            if (requirement.PmsProjectId != projectId)
                return BaseErrType.DataNotFound;

            var data = await _repository.GetWithContactAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;

            var existsMembers = await _memberContactRepository.GetListAsync(data.Id);
            var deleleMembers = existsMembers.Where(w => !form.UserIds.Contains(w.SysUserId)).ToList();

            var members = await _projectMemberContactRepository.GetListByProjectAsync(projectId);
            members = members.Where(w => form.UserIds.Contains(w.SysUserId) && !existsMembers.Any(w2 => w2.SysUserId == w.SysUserId)).ToList();

            var existsFiles = await _fileRepository.GetListAsync(data.Id);
            var deleleFiles = existsFiles.Where(w => !form.Files.Select(s => s.Id).Contains(w.Id)).ToList();

            var files = _mapper.Map<IEnumerable<PmsTaskFileForm>, IEnumerable<PmsTaskFile>>(form.Files);

            _mapper.Map(form, data);
            data.UpdateTime = DateTime.Now;

            var result = BaseErrType.Fail;
            var aggregate = new PmsTaskAggregate() { Task = data };
            aggregate.AssignSubTasks(members);
            aggregate.AddFiles(files);
            var contacts = aggregate.MemberContacts.Select(s => s.Contact).ToList();

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.UpdateAsync(data, tran);
                await _memberContactRepository.DeleteRangeAsync(deleleMembers, tran);
                await _memberContactRepository.AddRangeAsync(contacts, tran);

                await _fileRepository.DeleteRangeAsync(deleleFiles, tran);
                await _fileRepository.AddRangeAsync(files, tran);
                result = await ResultAsync(tran.CommitAsync);
            }

            if (result == BaseErrType.Success)
            {
                await Task.Run(() => aggregate.DeleteFileResource(deleleFiles));
            }

            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            data = data.Where(w => w.PmsProjectId == projectId).ToList();
            ids = data.Select(s => s.Id).ToList();

            var contactAggrs = await _memberContactRepository.GetListByTaskAsync(ids, true);
            var contacts = contactAggrs.Select(s => s.Contact).ToList();
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.DeleteRangeAsync(data, tran);
                await _memberContactRepository.DeleteRangeAsync(contacts, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <param name="filename">项目名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid projectId, Guid id, string filename, Stream file)
        {
            var result = new UploadResult() { Original = filename, Title = filename };
            var maxSize = 2 * 1024 * 1024;
            var task = await _repository.FindAsync(id);
            if (task == null)
            {
                task = new PmsTask();
                task.Id = id;
                task.PmsProjectId = projectId;
            }
            if (new ValidateImageType().Validate(filename, file))
            {
                result = await _uploader.WriteAsync(file, UPLOAD_PATH.Fmt(task.PmsProjectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename, maxSize) as UploadResult;
                // 设置返回虚拟路径
                if (result.State.Equals(UploadEnum.Success))
                {
                    result.Url = Path.Combine(VIRTUAL_PATH.Fmt(task.PmsProjectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename);
                }
            }
            else
            {
                result.State = UploadEnum.TypeError;
            }
            return result;
        }

        /// <summary>
        /// 修改用户任务
        /// </summary>
        /// <param name="id">任务id</param>
        /// <param name="members">子任务集合</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateTaskUsersAsync(Guid id, IEnumerable<PmsTaskChangeStatusForm> members)
        {
            var task = await _repository.GetWithContactAsync(id);
            if (task == null) return BaseErrType.DataNotFound;

            members.ForEach(e =>
            {
                //if (task.PmsTaskMemberContacts.Any(w => w.Id.Equals(e.Id)))
                //{
                //    var member = task.PmsTaskMemberContacts.FirstOrDefault(w => w.Id.Equals(e.Id));
                //    var changed = member.Status != e.Status;
                //    member.MapFrom(e);
                //    if (changed)
                //    {
                //        task.AddRecord(loginUser, member);
                //        member.ChangeTime();
                //        task.ChangeStatus(member);
                //    }
                //    member.CalculateActualHours(task.PmsTaskRecords);
                //}
            });
            return await ResultAsync(() => _repository.UpdateAsync(task));
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <param name="filename">项目名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadFileAsync(Guid projectId, Guid id, string filename, Stream file)
        {
            var result = new UploadResult() { Original = filename, Title = filename };
            var maxSize = 20 * 1024 * 1024;
            var task = await _repository.FindAsync(id);
            if (task == null)
            {
                task = new PmsTask();
                task.Id = id;
                task.PmsProjectId = projectId;
            }
            if (new ValidateImageType().Validate(filename, file) ||
                new ValidateDocType().Validate(filename, file) ||
                new ValidateZipType().Validate(filename, file))
            {
                result = await _uploader.WriteAsync(file, UPLOAD_PATH.Fmt(task.PmsProjectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename, maxSize) as UploadResult;
                // 设置返回虚拟路径
                if (result.State.Equals(UploadEnum.Success))
                {
                    result.Url = Path.Combine(VIRTUAL_PATH.Fmt(task.PmsProjectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename);
                }
            }
            else
            {
                result.State = UploadEnum.TypeError;
            }
            return result;
        }
    }
}
