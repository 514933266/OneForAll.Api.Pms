using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using Pms.Domain.Aggregates;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 任务记录
    /// </summary>
    public interface IPmsTaskRecordRepository : IEFCoreRepository<PmsTaskRecord>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<PmsTaskRecord>> GetListAsync(Guid taskId);
    }
}
