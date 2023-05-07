using OneForAll.Core;
using Pms.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Interfaces
{
    /// <summary>
    /// 项目成员
    /// </summary>
    public interface IPmsMemberService
    {
        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<PmsProjectMemberDto>> GetListAsync(Guid projectId);

        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="userIds">用户id集合</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, IEnumerable<Guid> userIds);

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="memberId">用户id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, Guid memberId);
    }
}
