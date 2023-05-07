using AutoMapper;
using NPOI.SS.Formula.Functions;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application
{
    /// <summary>
    /// 项目风险
    /// </summary>
    public class PmsRiskService : IPmsRiskService
    {
        private readonly IMapper _mapper;
        private readonly IPmsRiskManager _manager;
        private readonly IPmsProjectManager _projectManager;
        public PmsRiskService(IMapper mapper,
            IPmsRiskManager manager,
            IPmsProjectManager projectManager)
        {
            _mapper = mapper;
            _manager = manager;
            _projectManager = projectManager;
        }

        /// <summary>
        /// 获取风险列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsRiskDto>> GetListAsync(Guid projectId)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetListAsync(projectId);
                return _mapper.Map<IEnumerable<PmsRisk>, IEnumerable<PmsRiskDto>>(data);
            }
            return new List<PmsRiskDto>();
        }

        /// <summary>
        /// 添加风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsRiskForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.AddAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 修改风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsRiskForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">风险id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.DeleteAsync(projectId, ids);
            }
            return BaseErrType.NotAllow;
        }
    }
}
