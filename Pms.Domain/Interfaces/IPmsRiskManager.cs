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
    /// 项目风险
    /// </summary>
    public interface IPmsRiskManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>任务分页</returns>
        Task<IEnumerable<PmsRisk>> GetListAsync(Guid projectId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>

        Task<BaseErrType> AddAsync(Guid projectId, PmsRiskForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsRiskForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);
    }
}
