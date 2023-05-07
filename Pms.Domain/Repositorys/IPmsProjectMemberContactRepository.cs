using Pms.Domain.AggregateRoots;
using OneForAll.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Repositorys
{
    /// <summary>
    /// 仓储：项目成员
    /// </summary>
    public interface IPmsProjectMemberContactRepository : IEFCoreRepository<PmsProjectMemberContact>
    {
        /// <summary>
        /// 查询项目成员列表
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsMember>> GetListByProjectAsync(Guid projectId);
    }
}
