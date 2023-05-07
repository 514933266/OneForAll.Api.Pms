using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 任务明细
    /// </summary>
    public interface IPmsTaskDetailManager
    {
        /// <summary>
        /// 获取任务指派明细
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">关键字</param>
        /// <returns></returns>
        Task<IEnumerable<PmsTaskDetailAggregate>> GetListAsync(Guid projectId, Guid id);
    }
}
