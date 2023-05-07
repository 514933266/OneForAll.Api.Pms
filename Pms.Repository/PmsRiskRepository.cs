using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core;
using OneForAll.Core.ORM;
using OneForAll.Core.Extension;

namespace Pms.Repository
{
    /// <summary>
    /// 仓储：项目风险
    /// </summary>
   public class PmsRiskRepository : Repository<PmsRisk>, IPmsRiskRepository
    {
        public PmsRiskRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>任务分页</returns>
        public async Task<IEnumerable<PmsRisk>> GetListAsync(Guid projectId)
        {
            var predicate = PredicateBuilder.Create<PmsRisk>(w => w.PmsProjectId.Equals(projectId));

            var total = await DbSet.CountAsync(predicate);

            return await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(o => o.CreateTime)
                .ToListAsync();

        }
    }
}
