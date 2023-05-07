using AutoMapper;
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
    /// 项目里程碑
    /// </summary>
    public class PmsMilestoneService : IPmsMilestoneService
    {
        private readonly IMapper _mapper;
        private readonly IPmsMilestoneManager _manager;
        private readonly IPmsProjectManager _projectManager;
        public PmsMilestoneService(IMapper mapper,
            IPmsMilestoneManager manager,
            IPmsProjectManager projectManager)
        {
            _mapper = mapper;
            _manager = manager;
            _projectManager = projectManager;
        }

        /// <summary>
        /// 获取里程碑列表
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <returns>里程碑列表</returns>
        public async Task<IEnumerable<PmsMilestoneDto>> GetListAsync(Guid projectId)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetListAsync(projectId);
                return _mapper.Map<IEnumerable<PmsMilestone>, IEnumerable<PmsMilestoneDto>>(data);
            }
            return new List<PmsMilestoneDto>();
        }

        /// <summary>
        /// 添加里程碑
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <param name="form">里程碑实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsMilestoneForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.AddAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 修改里程碑
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <param name="form">里程碑实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsMilestoneForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除里程碑
        /// </summary>
        /// <param name="projectId">项目projectId</param>
        /// <param name="milestoneIds">里程碑projectId</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> milestoneIds)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.DeleteAsync(projectId, milestoneIds);
            }
            return BaseErrType.NotAllow;
        }
    }
}
