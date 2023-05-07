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
    /// 代码生成日志
    /// </summary>
    public class PmsCodeDenerationRecordRepository : Repository<PmsCodeDenerationRecord>, IPmsCodeDenerationRecordRepository
    {
        public PmsCodeDenerationRecordRepository(DbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>人员列表</returns>
        public async Task<IEnumerable<PmsCodeDenerationRecord>> GetListAsync(Guid projectId)
        {
            return await DbSet.OrderByDescending(o => o.CreateTime).Skip(0).Take(10).ToListAsync();
        }
    }
}
