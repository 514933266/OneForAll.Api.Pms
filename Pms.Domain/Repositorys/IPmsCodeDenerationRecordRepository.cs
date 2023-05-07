using OneForAll.EFCore;
using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 代码生成记录
    /// </summary>
    public interface IPmsCodeDenerationRecordRepository : IEFCoreRepository<PmsCodeDenerationRecord>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<PmsCodeDenerationRecord>> GetListAsync(Guid projectId);
    }
}
