using Pms.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：需求
    /// </summary>
    public interface IPmsRequirementRepository : IEFCoreRepository<PmsRequirement>
    {
        #region 分页

        /// <summary>
        /// 查询实体分页
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<PmsRequirement>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询实体分页
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<PmsRequirement>> GetPageAsync(IEnumerable<Guid> ids, int pageIndex, int pageSize, string key);

        #endregion

        #region 列表

        /// <summary>
        /// 查询项目实体列表
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsRequirement>> GetListAsync(Guid projectId);

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体Id</param>
        /// <returns>实体</returns>
        Task<PmsRequirement> GetAsync(Guid projectId, Guid id);

        /// <summary>
        /// 查询实体（含历史记录）
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>实体</returns>
        Task<PmsRequirement> GetWithRecordsAsync(Guid id);

        /// <summary>
        /// 根据标题查询项目实体
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="title">标题</param>
        /// <returns>实体</returns>
        Task<PmsRequirement> GetByTitleAsync(Guid projectId, string title);

        #endregion
    }
}
