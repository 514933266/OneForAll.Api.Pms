using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：用户
    /// </summary>
    public interface IPmsMemberRepository : IEFCoreRepository<PmsMember>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="ids">实体id集合</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsMember>> GetListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 查询项目成员列表
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsMember>> GetListByProjectAsync(Guid projectId);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="userId">系统用户id</param>
        /// <returns>列表</returns>
        Task<PmsMember> GetByUserAsync(Guid userId);

    }
}
