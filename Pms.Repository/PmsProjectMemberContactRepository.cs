using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Pms.Repository
{
    /// <summary>
    /// 仓储：项目成员
    /// </summary>
    public class PmsProjectMemberContactRepository : Repository<PmsProjectMemberContact>, IPmsProjectMemberContactRepository
    {
        public PmsProjectMemberContactRepository(DbContext context)
            : base(context)
        {

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
                       select new PmsMember()
                       {
                           Id = contact.Id,
                           Job = member.Job,
                           Name = member.Name,
                           SysTenantId = member.SysTenantId,
                           SysUserId = member.SysUserId,
                           UserName = member.UserName,
                           CreateTime = member.CreateTime
                       };

            return await data.AsNoTracking().ToListAsync();
        }
    }
}
