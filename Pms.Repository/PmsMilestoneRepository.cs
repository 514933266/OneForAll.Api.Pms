using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Repository
{
    /// <summary>
    /// 里程碑
    /// </summary>
    public class PmsMilestoneRepository : Repository<PmsMilestone>, IPmsMilestoneRepository
    {
        public PmsMilestoneRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsMilestone>> GetListAsync(Guid projectId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => w.PmsProjectId.Equals(projectId))
                .OrderByDescending(o => o.CreateTime)
                .ToListAsync();
        }
    }
}
