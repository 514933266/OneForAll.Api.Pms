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
    /// <summary>
    /// 项目Bug
    /// </summary>
    public interface IPmsBugManager
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>Bug分页</returns>
        Task<PageList<PmsBugAggregate>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsBugForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsBugForm form);

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
        /// <param name="projectId">仓库id</param>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        Task<IUploadResult> UploadImageAsync(Guid projectId, Guid id, string filename, Stream file);
    }
}
