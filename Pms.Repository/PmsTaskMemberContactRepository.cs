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
using Pms.Domain.Aggregates;

namespace Pms.Repository
{
    /// <summary>
    /// 仓储：任务详情
    /// </summary>
    public class PmsTaskMemberContactRepository : Repository<PmsTaskMemberContact>, IPmsTaskMemberContactRepository
    {
        public PmsTaskMemberContactRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<PmsTaskMemberContact>> GetListAsync(Guid taskId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => w.PmsTaskId.Equals(taskId))
                .ToListAsync();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskIds">任务id</param>
        /// <param name="isTracking">是否跟踪数据</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<PmsTaskMemberContactAggregate>> GetListByTaskAsync(IEnumerable<Guid> taskIds, bool isTracking = false)
        {
            var memberDbSet = Context.Set<PmsMember>();
            var contactDbSet = Context.Set<PmsTaskMemberContact>();
            var data = from contact in contactDbSet
                       join member in memberDbSet on contact.SysUserId equals member.SysUserId
                       where taskIds.Contains(contact.PmsTaskId)
                       select new PmsTaskMemberContactAggregate()
                       {
                           Contact = contact,
                           Member = member
                       };

            if (isTracking)
            {
                return await data.ToListAsync();
            }
            else
            {
                return await data.AsNoTracking().ToListAsync();
            }
        }
    }
}
