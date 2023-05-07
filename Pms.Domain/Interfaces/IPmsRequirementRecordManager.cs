using Pms.Domain.AggregateRoots;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 需求历史
    /// </summary>
    public interface IPmsRequirementRecordManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="requirementId">需求</param>
        /// <returns>历史列表</returns>
        Task<IEnumerable<PmsRequirementRecord>> GetListAsync(Guid requirementId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="requirementId">需求id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid requirementId, Guid id);
    }
}
