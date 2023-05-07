using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.ORM;
using OneForAll.Core.Extension;
using Pms.Domain.Enums;
using Pms.Domain.Aggregates;

namespace Pms.Repository
{
    /// <summary>
    /// 仓储：项目
    /// </summary>
    public class PmsProjectRepository : Repository<PmsProject>, IPmsProjectRepository
    {
        public PmsProjectRepository(DbContext context)
            : base(context)
        {

        }

        #region 列表

        /// <summary>
        /// 查询项目列表（成员能查看的）
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <param name="name">项目名称</param>
        /// <returns>项目</returns>
        public async Task<IEnumerable<PmsProject>> GetListPersonalAsync(Guid loginUserId, string name)
        {
            var predicate = PredicateBuilder.Create<PmsProject>(w => true);
            if (!name.IsNullOrEmpty())
            {
                predicate = predicate.And(w => w.Name.Contains(name));
            }

            var dbSet = DbSet.Where(predicate);
            var memberDbSet = Context.Set<PmsMember>();
            var contactDbSet = Context.Set<PmsProjectMemberContact>();

            var sql = (from project in dbSet
                       join contact in contactDbSet on project.Id equals contact.PmsProjectId
                       join member in memberDbSet on contact.PmsMemberId equals member.Id
                       where member.SysUserId == loginUserId
                       select project)
                       .Union(from project in dbSet
                              where project.CreatorId == loginUserId
                              select project);

            return await sql.ToListAsync();
        }

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <param name="id">项目id</param>
        /// <returns>项目</returns>
        public async Task<PmsProject> GetAsync(Guid loginUserId, Guid id)
        {
            return await DbSet
               .Where(w => w.Id.Equals(id) && w.CreatorId.Equals(loginUserId))
               .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询实体（含任务、任务详情、Bug、用户）
        /// </summary>
        /// <param name="id">项目id</param>
        /// <returns>项目</returns>
        public async Task<PmsProject> GetWithDetailsAsync(Guid id)
        {
            return await DbSet
               .Where(w => w.Id.Equals(id))
               .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询实体（含工作量-成本估算）
        /// </summary>
        /// <param name="id">项目id</param>
        /// <returns>项目</returns>
        public async Task<PmsProject> GetWithCostsAsync(Guid id)
        {
            return await DbSet
              .Where(w => w.Id.Equals(id))
              .FirstOrDefaultAsync();
        }

        #endregion
    }
}
