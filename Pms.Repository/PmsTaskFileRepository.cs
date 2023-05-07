using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Pms.Repository
{
    /// <summary>
    /// 仓储：任务附件
    /// </summary>
    public class PmsTaskFileRepository : Repository<PmsTaskFile>, IPmsTaskFileRepository
    {
        public PmsTaskFileRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<PmsTaskFile>> GetListAsync(Guid taskId)
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
        /// <returns>结果</returns>
        public async Task<IEnumerable<PmsTaskFile>> GetListByTaskAsync(IEnumerable<Guid> taskIds)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => taskIds.Contains(w.PmsTaskId))
                .ToListAsync();
        }
    }
}
