using AutoMapper;
using AutoMapper.Execution;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application
{
    /// <summary>
    /// 项目成员
    /// </summary>
    public class PmsMemberService : IPmsMemberService
    {
        private readonly IMapper _mapper;
        private readonly IPmsMemberManager _manager;
        private readonly IPmsProjectManager _projectManager;
        public PmsMemberService(
            IMapper mapper,
            IPmsMemberManager manager,
            IPmsProjectManager projectManager
            )
        {
            _mapper = mapper;
            _manager = manager;
            _projectManager = projectManager;
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<PmsProjectMemberDto>> GetListAsync(Guid projectId)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetListAsync(projectId);
                return _mapper.Map<IEnumerable<PmsMember>, IEnumerable<PmsProjectMemberDto>>(data);
            }
            return new List<PmsProjectMemberDto>();
        }

        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="userIds">用户id集合</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, IEnumerable<Guid> userIds)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.AddAsync(projectId, userIds);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="memberId">用户id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, Guid memberId)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.DeleteAsync(projectId, memberId);
            }
            return BaseErrType.NotAllow;
        }
    }
}
