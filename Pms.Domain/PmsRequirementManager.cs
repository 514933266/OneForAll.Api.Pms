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
using System.Collections.Immutable;

namespace Pms.Domain
{
    /// <summary>
    /// 实体
    /// </summary>
    public class PmsRequirementManager : PmsBaseManager, IPmsRequirementManager
    {
        // 文件存储路径
        private readonly string UPLOAD_PATH = "upload/projects/{0}/requirements/{1}/";
        // 虚拟路径：根据Startup配置设置,返回给前端访问资源
        private readonly string VIRTUAL_PATH = "resources/projects/{0}/requirements/{1}/";

        private readonly IUploader _uploader;
        private readonly IPmsRequirementRepository _reposiotry;
        private readonly IPmsRequirementRecordRepository _recordRepository;
        public PmsRequirementManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUploader uploader,
            IPmsRequirementRepository reposiotry,
            IPmsRequirementRecordRepository recordRepository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _reposiotry = reposiotry;
            _recordRepository = recordRepository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>实体分页</returns>
        public async Task<PageList<PmsRequirement>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _reposiotry.GetPageAsync(projectId, pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>实体分页</returns>
        public async Task<IEnumerable<PmsRequirement>> GetListAsync(Guid projectId)
        {
            return await _reposiotry.GetListAsync(projectId);
        }

        /// <summary>
        /// 获取需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <returns>需求列表</returns>
        public async Task<PmsRequirement> GetAsync(Guid projectId, Guid id)
        {
            return await _reposiotry.GetAsync(projectId, id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsRequirementForm form)
        {
            var data = await _reposiotry.GetByTitleAsync(projectId, form.Title);
            if (data != null)
                return BaseErrType.DataExist;

            data = _mapper.Map<PmsRequirementForm, PmsRequirement>(form);
            data.PmsProjectId = projectId;
            data.UpdateTime = DateTime.Now;
            data.CreateTime = DateTime.Now;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;

            // 添加实体历史记录
            var record = _mapper.Map<PmsRequirement, PmsRequirementRecord>(data);
            record.VersionRemark = "初始版本";

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _reposiotry.AddAsync(data, tran);
                record.PmsRequirementId = data.Id;
                await _recordRepository.AddAsync(record, tran);
                return await ResultAsync(tran.CommitAsync);
            };
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsRequirementForm form)
        {
            var data = await _reposiotry.GetByTitleAsync(projectId, form.Title);
            if (data != null && data.Id != form.Id)
                return BaseErrType.DataExist;

            _mapper.Map(form, data);
            data.UpdateTime = DateTime.Now;

            // 添加实体历史记录
            var record = _mapper.Map<PmsRequirement, PmsRequirementRecord>(data);
            record.Id = Guid.Empty;
            record.PmsRequirementId = data.Id;
            record.VersionRemark = form.VersionRemark;

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _reposiotry.UpdateAsync(data, tran);
                await _recordRepository.AddAsync(record, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var data = await _reposiotry.GetListAsync(w => ids.Contains(w.Id));
            var records = await _recordRepository.GetListByRequirementAsync(ids);
            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _reposiotry.DeleteRangeAsync(data, tran);
                await _recordRepository.DeleteRangeAsync(records, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 上传实体图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="filename">项目名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid projectId, string filename, Stream file)
        {
            var result = new UploadResult() { State = UploadEnum.Fail, Original = filename, Title = filename };
            var maxSize = 2 * 1024 * 1024;

            if (new ValidateImageType().Validate(filename, file))
            {
                result = await _uploader.WriteAsync(file, UPLOAD_PATH.Fmt(projectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename, maxSize) as UploadResult;
                // 设置返回虚拟路径
                if (result.State.Equals(UploadEnum.Success))
                {
                    result.Url = Path.Combine(VIRTUAL_PATH.Fmt(projectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename);
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
