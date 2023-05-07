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

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IPmsRequirementManager
    {
        /// <summary>
        /// 获取实体分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>实体分页</returns>
        Task<PageList<PmsRequirement>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>实体分页</returns>
        Task<IEnumerable<PmsRequirement>> GetListAsync(Guid projectId);

        /// <summary>
        /// 获取需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <returns>需求列表</returns>
        Task<PmsRequirement> GetAsync(Guid projectId, Guid id);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsRequirementForm form);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsRequirementForm form);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);

        /// <summary>
        /// 上传实体图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="filename">项目名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadImageAsync(Guid projectId, string filename, Stream file);
    }
}
