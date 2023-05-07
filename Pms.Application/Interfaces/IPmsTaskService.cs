using OneForAll.Core.Upload;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Interfaces
{
    /// <summary>
    /// 项目任务
    /// </summary>
    public interface IPmsTaskService
    {
        /// <summary>
        /// 获取任务分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>任务分页</returns>
        Task<PageList<PmsTaskDto>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">任务实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsTaskForm form);

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">任务实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsTaskForm form);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskIds">任务实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> taskIds);

        /// <summary>
        /// 修改子任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskId">任务id</param>
        /// <param name="users">子任务集合</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateTaskUsersAsync(Guid projectId, Guid taskId, IEnumerable<PmsTaskChangeStatusForm> users);

        /// <summary>
        /// 上传任务图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskId">任务id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadImageAsync(Guid projectId, Guid taskId, string filename, Stream file);

        /// <summary>
        /// 上传任务附件
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskId">任务id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadFileAsync(Guid projectId, Guid taskId, string filename, Stream file);

        #region 任务明细

        /// <summary>
        /// 获取任务指派明细
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">关键字</param>
        /// <returns></returns>
        Task<IEnumerable<PmsTaskDetailDto>> GetListDetailAsync(Guid projectId, Guid id);

        #endregion
    }
}
