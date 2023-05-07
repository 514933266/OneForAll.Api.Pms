using Pms.Domain.AggregateRoots;
using Pms.Domain.Repositorys;
using Microsoft.EntityFrameworkCore;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Pms.Domain.Aggregates;

namespace Pms.Repository
{
    /// <summary>
    /// 任务记录
    /// </summary>
    public class PmsTaskRecordRepository : Repository<PmsTaskRecord>, IPmsTaskRecordRepository
    {
        public PmsTaskRecordRepository(DbContext context)
           : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<PmsTaskRecord>> GetListAsync(Guid taskId)
        {
            return await DbSet
                .AsNoTracking()
                .Where(w => w.PmsTaskId.Equals(taskId))
                .OrderByDescending(o => o.CreateTime)
                .ToListAsync();
        }
    }
}
