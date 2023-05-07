using Pms.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 项目统计
    /// </summary>
    public interface IPmsStatisticsManager
    {
        /// <summary>
        /// 获取项目成员任务统计
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        Task<IEnumerable<PmsMemberTaskStatistics>> GetMemberTasksAsync(Guid projectId);
    }
}
