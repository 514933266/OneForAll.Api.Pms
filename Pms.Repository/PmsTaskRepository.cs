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
using Pms.Domain.Enums;

namespace Pms.Repository
{
    /// <summary>
    /// 仓储：项目任务
    /// </summary>
    public class PmsTaskRepository : Repository<PmsTask>, IPmsTaskRepository
    {
        public PmsTaskRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>任务分页</returns>
        public async Task<PageList<PmsTask>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<PmsTask>(w => w.PmsProjectId.Equals(projectId));
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Title.Contains(key));

            var total = await DbSet
                .CountAsync(predicate);

            var items = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(o => o.CreateTime)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageList<PmsTask>(total, pageIndex, pageSize, items);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>实体</returns>
        public async Task<IEnumerable<PmsTask>> GetListByProjectAsync(Guid projectId)
        {
            return await DbSet
                .Where(w => w.PmsProjectId == projectId)
                .ToListAsync();
        }

        /// <summary>
        /// 查询个人未完成任务列表
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <returns>实体</returns>
        public async Task<IEnumerable<PmsTask>> GetListPersonalUnFinishedAsync(Guid loginUserId)
        {
            var contactDbSet = Context.Set<PmsTaskMemberContact>();
            var taskDbSet = Context.Set<PmsTask>();

            var sql = (from task in taskDbSet
                       join contact in contactDbSet on task.Id equals contact.PmsTaskId
                       where contact.SysUserId == loginUserId && contact.Status < Domain.Enums.PmsTaskStatusEnum.Finish
                       select task);

            return await sql.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 查询实体（含子任务、历史关联表数据）
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>实体</returns>
        public async Task<PmsTask> GetWithContactAsync(Guid id)
        {
            return await DbSet
                .Where(w => w.Id == id)
                //.Include(e => e.PmsTaskMemberContacts)
                //.Include(e => e.PmsTaskRecords)
                //.Include(e => e.PmsTaskFiles)
                .FirstOrDefaultAsync();
        }
    }
}
