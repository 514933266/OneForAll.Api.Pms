using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core;
using Pms.Domain.Aggregates;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：任务详情
    /// </summary>
    public interface IPmsTaskMemberContactRepository : IEFCoreRepository<PmsTaskMemberContact>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<PmsTaskMemberContact>> GetListAsync(Guid taskId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskIds">任务id</param>
        /// <param name="isTracking">是否跟踪数据</param>
        /// <returns>结果</returns>
        Task<IEnumerable<PmsTaskMemberContactAggregate>> GetListByTaskAsync(IEnumerable<Guid> taskIds, bool isTracking = false);
    }
}
