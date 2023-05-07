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
    /// 仓储：项目需求
    /// </summary>
    public class PmsRequirementRepository : Repository<PmsRequirement>, IPmsRequirementRepository
    {
        public PmsRequirementRepository(DbContext context)
            : base(context)
        {

        }

        #region 分页

        /// <summary>
        /// 查询实体分页
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<PmsRequirement>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<PmsRequirement>(w => w.PmsProjectId.Equals(projectId));
            if (!key.IsNullOrEmpty()) predicate = predicate.And<PmsRequirement>(w => w.Title.Contains(key));

            var total = await DbSet
                .AsNoTracking()
                .CountAsync(predicate);

            var items = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(o => o.CreateTime)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageList<PmsRequirement>(total, pageIndex, pageSize, items);
        }

        /// <summary>
        /// 查询实体分页
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<PmsRequirement>> GetPageAsync(IEnumerable<Guid> ids, int pageIndex, int pageSize, string key)
        {
            var predicate = PredicateBuilder.Create<PmsRequirement>(w => ids.Contains(w.Id));
            if (!key.IsNullOrEmpty()) predicate = predicate.And(w => w.Title.Contains(key));

            var total = await DbSet
                .AsNoTracking()
                .CountAsync(predicate);

            var items = await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(o => o.CreateTime)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PageList<PmsRequirement>(total, pageIndex, pageSize, items);
        }

        #endregion

        #region 列表

        /// <summary>
        /// 查询项目实体列表
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsRequirement>> GetListAsync(Guid projectId)
        {
            var predicate = PredicateBuilder.Create<PmsRequirement>(w => w.PmsProjectId.Equals(projectId));

            return await DbSet
                .AsNoTracking()
                .Where(predicate)
                .OrderByDescending(o => o.CreateTime)
                .ToListAsync();
        }

        #endregion

        #region 实体

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体Id</param>
        /// <returns>实体</returns>
        public async Task<PmsRequirement> GetAsync(Guid projectId, Guid id)
        {
            return await DbSet
                .Where(w => w.Id.Equals(id) && w.PmsProjectId.Equals(projectId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查询实体（含历史记录）
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>实体</returns>
        public async Task<PmsRequirement> GetWithRecordsAsync(Guid id)
        {
            return await DbSet
                .Where(w => w.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 根据标题查询项目实体
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="title">标题</param>
        /// <returns>实体</returns>
        public async Task<PmsRequirement> GetByTitleAsync(Guid projectId, string title)
        {
            return await DbSet
                .Where(w => w.PmsProjectId.Equals(projectId) && w.Title.Equals(title))
                .FirstOrDefaultAsync();
        }

        #endregion
    }
}
