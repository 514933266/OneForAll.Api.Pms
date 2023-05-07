using AutoMapper;
using OneForAll.Core.Extension;
using OneForAll.Core.Upload;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pms.Domain.Aggregates;
using NPOI.SS.Formula.Functions;
using NPOI.HPSF;

namespace Pms.Application
{
    /// <summary>
    /// 项目任务
    /// </summary>
    public class PmsTaskService : IPmsTaskService
    {
        private readonly IMapper _mapper;
        private readonly IPmsTaskManager _manager;
        private readonly IPmsTaskDetailManager _detailManager;
        private readonly IPmsProjectManager _projectManager;
        public PmsTaskService(
            IMapper mapper,
            IPmsTaskManager manager,
            IPmsTaskDetailManager detailManager,
            IPmsProjectManager projectManager
            )
        {
            _mapper = mapper;
            _manager = manager;
            _detailManager = detailManager;
            _projectManager = projectManager;
        }

        /// <summary>
        /// 获取任务分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>任务分页</returns>
        public async Task<PageList<PmsTaskDto>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetPageAsync(projectId, pageIndex, pageSize, key);
                var tasks = data.Items.Select(s => s.Task).ToList();
                var items = _mapper.Map<IEnumerable<PmsTask>, IEnumerable<PmsTaskDto>>(tasks);
                items.ForEach(e =>
                {
                    var item = data.Items.First(w => w.Task.Id == e.Id);
                    e.UserIds = item.MemberContacts.Select(s => s.Member.SysUserId).ToList();
                    e.Files = _mapper.Map<ICollection<PmsTaskFile>, ICollection<PmsTaskFileDto>>(item.Files);
                });
                return new PageList<PmsTaskDto>(data.Total, data.PageIndex, data.PageSize, items);
            }
            return new PageList<PmsTaskDto>(0, pageIndex, pageSize, new List<PmsTaskDto>());
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">任务实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsTaskForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.AddAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">任务实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsTaskForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskIds">任务实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> taskIds)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.DeleteAsync(projectId, taskIds);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 修改子任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskId">任务id</param>
        /// <param name="users">子任务集合</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateTaskUsersAsync(Guid projectId, Guid taskId, IEnumerable<PmsTaskChangeStatusForm> users)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateTaskUsersAsync(taskId, users);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 上传任务图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskId">任务id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid projectId, Guid taskId, string filename, Stream file)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UploadImageAsync(projectId, taskId, filename, file);
            }
            return default;
        }

        /// <summary>
        /// 上传任务附件
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskId">任务id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadFileAsync(Guid projectId, Guid taskId, string filename, Stream file)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UploadFileAsync(projectId, taskId, filename, file);
            }
            return default;
        }

        #region 任务明细

        /// <summary>
        /// 获取任务指派明细
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">关键字</param>
        /// <returns></returns>
        public async Task<IEnumerable<PmsTaskDetailDto>> GetListDetailAsync(Guid projectId, Guid id)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _detailManager.GetListAsync(projectId, id);
                return _mapper.Map<IEnumerable<PmsTaskDetailAggregate>, IEnumerable<PmsTaskDetailDto>>(data);
            }
            return new List<PmsTaskDetailDto>();
        }

        #endregion
    }
}
