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
    /// 代码结构
    /// </summary>
    public class PmsCodeStructureRepository : Repository<PmsCodeStructure>, IPmsCodeStructureRepository
    {
        public PmsCodeStructureRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<PmsCodeStructure>> GetListAsync(Guid projectId, string key)
        {
            var predicate = PredicateBuilder.Create<PmsCodeStructure>(w => w.PmsProjectId.Equals(projectId));
            if (!key.IsNullOrEmpty())
                predicate = predicate.And(w => w.Name.Contains(key));

            return await DbSet.Where(predicate).OrderBy(o => o.Name).ThenByDescending(o => o.Type).ToListAsync();
        }
    }
}
