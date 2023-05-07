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
    /// 里程碑
    /// </summary>
    public interface IPmsMilestoneRepository : IEFCoreRepository<PmsMilestone>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsMilestone>> GetListAsync(Guid projectId);
    }
}
