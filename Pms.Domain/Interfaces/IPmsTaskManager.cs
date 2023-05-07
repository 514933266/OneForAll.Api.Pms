using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;
using Pms.Domain.Aggregates;

namespace Pms.Domain.Interfaces
{
    public interface IPmsTaskManager
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>任务分页</returns>
        Task<PageList<PmsTaskAggregate>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsTaskForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsTaskForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>

        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <param name="filename">项目名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadImageAsync(Guid projectId, Guid id, string filename, Stream file);

        /// <summary>
        /// 修改用户任务
        /// </summary>
        /// <param name="id">任务id</param>
        /// <param name="users">子任务集合</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateTaskUsersAsync(Guid id, IEnumerable<PmsTaskChangeStatusForm> users);

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <param name="filename">项目名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadFileAsync(Guid projectId, Guid id, string filename, Stream file);
    }
}
