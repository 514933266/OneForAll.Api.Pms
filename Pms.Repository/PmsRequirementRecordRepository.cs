using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Repository
{
    /// <summary>
    /// 仓储：需求历史
    /// </summary>
    public class PmsRequirementRecordRepository : Repository<PmsRequirementRecord>, IPmsRequirementRecordRepository
    {
        public PmsRequirementRecordRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="requirementId">需求id</param>
        /// <returns>历史列表</returns>
        public async Task<IEnumerable<PmsRequirementRecord>> GetListAsync(Guid requirementId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => w.PmsRequirementId.Equals(requirementId))
                .OrderByDescending(o => o.CreateTime)
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="requirementIds">需求id</param>
        /// <returns>历史列表</returns>
        public async Task<IEnumerable<PmsRequirementRecord>> GetListByRequirementAsync(IEnumerable<Guid> requirementIds)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => requirementIds.Contains(w.PmsRequirementId))
                .ToListAsync();
        }
    }
}
