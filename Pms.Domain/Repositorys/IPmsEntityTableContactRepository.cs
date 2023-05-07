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
    /// 表关联信息
    /// </summary>
    public interface IPmsEntityTableContactRepository : IEFCoreRepository<PmsEntityTableContact>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="sourceId">主表id</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<PmsEntityTableContact>> GetListAsync(Guid projectId, Guid sourceId);
    }
}
