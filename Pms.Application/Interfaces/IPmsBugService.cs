using OneForAll.Core.Upload;
using OneForAll.Core;
using Pms.Application.Dtos;
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
    /// 项目bug
    /// </summary>
    public interface IPmsBugService
    {
        /// <summary>
        /// 获取Bug分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>Bug分页</returns>
        Task<PageList<PmsBugDto>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 添加Bug
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">Bug实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsBugForm form);

        /// <summary>
        /// 修改Bug
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">Bug实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsBugForm form);

        /// <summary>
        /// 删除Bug
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">Bug id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);

        /// <summary>
        /// 上传Bug图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">Bug Id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadImageAsync(Guid projectId, Guid id, string filename, Stream file);

    }
}
