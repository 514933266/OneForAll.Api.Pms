using Pms.Domain.AggregateRoots;
using OneForAll.Core;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pms.Domain.Aggregates;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：Bug
    /// </summary>
    public interface IPmsBugRepository : IEFCoreRepository<PmsBug>
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<PageList<PmsBugAggregate>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询个人分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="projectId">项目id</param>
        /// <param name="userId">登录用户id</param>
        /// <param name="key">关键字</param>
        /// <returns>结果</returns>
        Task<PageList<PmsBug>> GetPagePersonalAsync(Guid projectId, Guid userId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询个人未完成Bug列表
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <returns>实体</returns>
        Task<IEnumerable<PmsBug>> GetListPersonalUnFinishedAsync(Guid loginUserId);
    }
}
