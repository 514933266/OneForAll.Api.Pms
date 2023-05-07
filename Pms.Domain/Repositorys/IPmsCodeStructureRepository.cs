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
    /// 代码结构
    /// </summary>
    public interface IPmsCodeStructureRepository : IEFCoreRepository<PmsCodeStructure>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<PmsCodeStructure>> GetListAsync(Guid projectId, string key);
    }
}
