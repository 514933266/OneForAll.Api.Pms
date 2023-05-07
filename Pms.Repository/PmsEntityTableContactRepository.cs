using Microsoft.EntityFrameworkCore;
using OneForAll.Core.ORM;
using OneForAll.EFCore;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Repository
{
    /// <summary>
    /// 表关联信息
    /// </summary>
    public class PmsEntityTableContactRepository : Repository<PmsEntityTableContact>, IPmsEntityTableContactRepository {
        public PmsEntityTableContactRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="sourceId">主表id</param>
        /// <returns>分页列表</returns>
        public async Task<IEnumerable<PmsEntityTableContact>> GetListAsync(Guid projectId, Guid sourceId)
        {
            var tableDbSet = Context.Set<PmsEntityTable>().Where(w => w.PmsProjectId == projectId);
            var sql = (from table in tableDbSet
                       join contact in DbSet on table.Id equals contact.SourceTableId
                       where contact.SourceTableId == sourceId || contact.TargetTableId == sourceId
                       select contact);

            return await sql.ToListAsync();
        }
    }
}
