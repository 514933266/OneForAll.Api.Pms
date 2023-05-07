using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain
{
    /// <summary>
    /// 需求历史
    /// </summary>
    public class PmsRequirementRecordManager : BaseManager, IPmsRequirementRecordManager
    {
        private readonly IPmsRequirementRecordRepository _reposiotry;
        public PmsRequirementRecordManager(IPmsRequirementRecordRepository reposiotry)
        {
            _reposiotry = reposiotry;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="requirementId">需求</param>
        /// <returns>历史列表</returns>
        public async Task<IEnumerable<PmsRequirementRecord>> GetListAsync(Guid requirementId)
        {
            return await _reposiotry.GetListAsync(requirementId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="requirementId">需求id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid requirementId, Guid id)
        {
            var record = await _reposiotry.FindAsync(id);
            if (record == null)
                return BaseErrType.DataNotFound;
            if (record.PmsRequirementId != requirementId)
                return BaseErrType.DataNotFound;

            return await ResultAsync(() => _reposiotry.DeleteAsync(record));
        }
    }
}
