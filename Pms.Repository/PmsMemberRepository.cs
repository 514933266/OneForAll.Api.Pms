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
    /// 用户
    /// </summary>
    public class PmsMemberRepository : Repository<PmsMember>, IPmsMemberRepository
    {
        public PmsMemberRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id集合</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsMember>> GetListAsync(IEnumerable<Guid> ids)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => ids.Contains(w.Id))
                .ToListAsync();
        }

        /// <summary>
        /// 查询项目成员列表
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsMember>> GetListByProjectAsync(Guid projectId)
        {
            var memberDbSet = Context.Set<PmsMember>();
            var contactDbSet = Context.Set<PmsProjectMemberContact>();
            var data = from contact in contactDbSet
                       join member in memberDbSet on contact.PmsMemberId equals member.Id
                       where contact.PmsProjectId == projectId
                       select member;

            return await data.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="userId">系统用户id</param>
        /// <returns>列表</returns>
        public async Task<PmsMember> GetByUserAsync(Guid userId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => w.SysUserId == userId)
                .FirstOrDefaultAsync();
        }
    }
}
