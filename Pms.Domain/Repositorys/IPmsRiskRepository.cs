using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：项目风险
    /// </summary>
    public interface IPmsRiskRepository : IEFCoreRepository<PmsRisk>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>任务分页</returns>
        Task<IEnumerable<PmsRisk>> GetListAsync(Guid projectId);
    }
}
