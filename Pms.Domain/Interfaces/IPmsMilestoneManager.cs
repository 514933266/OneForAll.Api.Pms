using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 里程碑
    /// </summary>
    public interface IPmsMilestoneManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsMilestone>> GetListAsync(Guid projectId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsMilestoneForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsMilestoneForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);
    }
}
