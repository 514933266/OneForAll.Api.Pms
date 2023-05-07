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
    /// 项目风险
    /// </summary>
    public interface IPmsRiskService
    {
        /// <summary>
        /// 获取风险列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsRiskDto>> GetListAsync(Guid projectId);

        /// <summary>
        /// 添加风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsRiskForm form);

        /// <summary>
        /// 修改风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsRiskForm form);

        /// <summary>
        /// 删除风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">风险id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);
    }
}
