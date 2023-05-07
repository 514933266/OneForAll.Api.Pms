using Pms.Domain.AggregateRoots;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pms.Domain.Models;
using Pms.Public.Models;

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 项目成员
    /// </summary>
    public interface IPmsMemberManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>成员列表</returns>
        Task<IEnumerable<PmsMember>> GetListAsync(Guid projectId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="userIds">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, IEnumerable<Guid> userIds);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, Guid id);
    }
}
