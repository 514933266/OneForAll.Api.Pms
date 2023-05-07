using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：项目
    /// </summary>
    public interface IPmsProjectRepository : IEFCoreRepository<PmsProject>
    {
        #region 列表

        /// <summary>
        /// 查询项目列表（成员能查看的）
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <param name="name">项目名称</param>
        /// <returns>项目</returns>
        Task<IEnumerable<PmsProject>> GetListPersonalAsync(Guid loginUserId, string name);

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <param name="id">项目id</param>
        /// <returns>项目</returns>
        Task<PmsProject> GetAsync(Guid loginUserId, Guid id);

        /// <summary>
        /// 查询实体（含任务、任务详情、Bug、用户）
        /// </summary>
        /// <param name="id">项目id</param>
        /// <returns>项目</returns>
        Task<PmsProject> GetWithDetailsAsync(Guid id);

        /// <summary>
        /// 查询实体（含工作量-成本估算）
        /// </summary>
        /// <param name="id">项目id</param>
        /// <returns>项目</returns>
        Task<PmsProject> GetWithCostsAsync(Guid id);

        #endregion
    }
}
