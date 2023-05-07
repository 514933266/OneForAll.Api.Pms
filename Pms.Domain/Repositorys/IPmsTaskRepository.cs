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
    /// 仓储：项目任务
    /// </summary>
    public interface IPmsTaskRepository : IEFCoreRepository<PmsTask>
    {
        /// <summary>
        /// 查询分页（含子任务、历史关联表数据）
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>任务分页</returns>
        Task<PageList<PmsTask>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>实体</returns>
        Task<IEnumerable<PmsTask>> GetListByProjectAsync(Guid projectId);

        /// <summary>
        /// 查询个人未完成任务列表
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <returns>实体</returns>
        Task<IEnumerable<PmsTask>> GetListPersonalUnFinishedAsync(Guid loginUserId);

        /// <summary>
        /// 查询实体（含子任务、历史关联表数据）
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        Task<PmsTask> GetWithContactAsync(Guid id);
    }
}
