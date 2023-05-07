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
    /// 表实体
    /// </summary>
    public interface IPmsEntityTableRepository : IEFCoreRepository<PmsEntityTable>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<PmsEntityTable>> GetListAsync(Guid projectId, string key);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">表id</param>
        /// <returns>分页列表</returns>
        Task<IEnumerable<PmsEntityTable>> GetListAsync(IEnumerable<Guid> ids);
    }
}
