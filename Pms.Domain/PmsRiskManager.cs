using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;
using Microsoft.AspNetCore.Http;

namespace Pms.Domain
{
    /// <summary>
    /// 项目风险
    /// </summary>
    public class PmsRiskManager : PmsBaseManager, IPmsRiskManager
    {
        private readonly IPmsRiskRepository _riskRepository;
        private readonly IPmsMilestoneRepository _milestoneRepository;

        public PmsRiskManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsRiskRepository riskRepository,
            IPmsMilestoneRepository milestoneRepository) : base(mapper, httpContextAccessor)
        {
            _riskRepository = riskRepository;
            _milestoneRepository = milestoneRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>任务分页</returns>
        public async Task<IEnumerable<PmsRisk>> GetListAsync(Guid projectId)
        {
            return await _riskRepository.GetListAsync(projectId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>

        public async Task<BaseErrType> AddAsync(Guid projectId, PmsRiskForm form)
        {
            var milestone = await _milestoneRepository.FindAsync(form.MilestoneId);
            if (milestone == null) return BaseErrType.DataError;

            var data = _mapper.Map<PmsRiskForm, PmsRisk>(form);
            data.Id = Guid.NewGuid();

            data.PmsProjectId = projectId;
            //data.Init(LoginUser);
            return await ResultAsync(() => _riskRepository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsRiskForm form)
        {
            var data = await _riskRepository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataError;
            if (data.PmsProjectId != projectId) return BaseErrType.DataError;
            var milestone = await _milestoneRepository.FindAsync(form.MilestoneId);
            if (milestone == null) return BaseErrType.DataError;

            _mapper.Map(form, data);
            return await ResultAsync(() => _riskRepository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var data = await _riskRepository.GetListAsync(w => ids.Contains(w.Id));
            if (data == null) return BaseErrType.DataError;
            if (data.Any(w => w.PmsProjectId != projectId)) return BaseErrType.DataError;

            return await ResultAsync(() => _riskRepository.DeleteRangeAsync(data));
        }
    }
}
