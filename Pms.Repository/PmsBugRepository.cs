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
    /// 仓储：Bug
    /// </summary>
    public class PmsBugRepository : Repository<PmsBug>, IPmsBugRepository
    {
        public PmsBugRepository(DbContext context)
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
        /// <returns>分页列表</returns>
        public async Task<PageList<PmsBugAggregate>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<PmsBug>(w => w.PmsProjectId.Equals(projectId));
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Title.Contains(key));

            var total = await DbSet.CountAsync(predicate);

            var dbSet = Context.Set<PmsBug>().Where(predicate);
            var memberDbSet = Context.Set<PmsMember>();

            var sql = from bug in dbSet
                      join member in memberDbSet on bug.SysUserId equals member.SysUserId
                      into leftJoinMember
                      from bugMember in leftJoinMember.DefaultIfEmpty()
                      orderby bug.CreateTime descending
                      select new PmsBugAggregate()
                      {
                          Bug = bug,
                          Member = bugMember
                      };

            var items = await sql
                .AsNoTracking()
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageList<PmsBugAggregate>(total, pageIndex, pageSize, items);
        }

        /// <summary>
        /// 查询个人分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="userId">登录用户id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>结果</returns>
        public async Task<PageList<PmsBug>> GetPagePersonalAsync(Guid projectId, Guid userId, int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<PmsBug>(w => w.PmsProjectId.Equals(projectId));
            predicate = predicate.And(w => w.SysUserId.Equals(userId));
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Title.Contains(key));

            var total = await DbSet
                .AsNoTracking()
                .CountAsync(predicate);

            var items = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(e => e.CreateTime)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageList<PmsBug>(total, pageIndex, pageSize, items);
        }

        /// <summary>
        /// 查询个人未完成Bug列表
        /// </summary>
        /// <param name="loginUserId">登录用户id</param>
        /// <returns>实体</returns>
        public async Task<IEnumerable<PmsBug>> GetListPersonalUnFinishedAsync(Guid loginUserId)
        {
            var predicate = PredicateBuilder.Create<PmsBug>(w => w.SysUserId.Equals(loginUserId) && w.Status == PmsBugStatusEnum.UnFinished);

            return await DbSet.Where(predicate).AsNoTracking().ToListAsync();
        }
    }
}
