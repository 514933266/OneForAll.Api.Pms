using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：需求历史
    /// </summary>
    public interface IPmsRequirementRecordRepository : IEFCoreRepository<PmsRequirementRecord>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="requirementId">需求id</param>
        /// <returns>历史列表</returns>
        Task<IEnumerable<PmsRequirementRecord>> GetListAsync(Guid requirementId);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="requirementIds">需求id</param>
        /// <returns>历史列表</returns>
        Task<IEnumerable<PmsRequirementRecord>> GetListByRequirementAsync(IEnumerable<Guid> requirementIds);
    }
}
