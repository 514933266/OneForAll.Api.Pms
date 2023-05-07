using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Interfaces
{
    /// <summary>
    /// 项目里程碑
    /// </summary>
    public interface IPmsMilestoneService
    {
        /// <summary>
        /// 获取里程碑列表
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <returns>里程碑列表</returns>
        Task<IEnumerable<PmsMilestoneDto>> GetListAsync(Guid projectId);

        /// <summary>
        /// 添加里程碑
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <param name="form">里程碑实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsMilestoneForm form);

        /// <summary>
        /// 修改里程碑
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <param name="form">里程碑实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsMilestoneForm form);

        /// <summary>
        /// 删除里程碑
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <param name="milestoneIds">里程碑projectId</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> milestoneIds);
    }
}
