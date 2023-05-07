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
using Pms.Domain.Aggregates;
using AutoMapper.Execution;
using Pms.Domain.Enums;

namespace Pms.Repository
{
    /// <summary>
    /// 表实体
    /// </summary>
    public class PmsEntityTableRepository : Repository<PmsEntityTable>, IPmsEntityTableRepository
    {
        public PmsEntityTableRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<PmsEntityTable>> GetListAsync(Guid projectId, string key)
        {
            var predicate = PredicateBuilder.Create<PmsEntityTable>(w => w.PmsProjectId.Equals(projectId));
            if (!key.IsNullOrEmpty())
                predicate = predicate.And(w => w.Name.Contains(key));

            return await DbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">表id</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<PmsEntityTable>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet.Where(w => ids.Contains(w.Id)).ToListAsync();
        }
    }
}
