using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Pms.Public.Models;
using Microsoft.AspNetCore.Http;

namespace Pms.Domain
{
    /// <summary>
    /// 里程碑
    /// </summary>
    public class PmsMilestoneManager : PmsBaseManager, IPmsMilestoneManager
    {
        private readonly IPmsMilestoneRepository _milestoneRepository;
        public PmsMilestoneManager(
            IMapper mapper,
            IPmsMilestoneRepository milestoneRepository,
            IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _milestoneRepository = milestoneRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsMilestone>> GetListAsync(Guid projectId)
        {
            return await _milestoneRepository.GetListAsync(projectId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsMilestoneForm form)
        {
            var data = _mapper.Map<PmsMilestoneForm, PmsMilestone>(form);
            data.Id = Guid.NewGuid();
            data.PmsProjectId = projectId;
            data.CreateTime = DateTime.Now;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            return await ResultAsync(() => _milestoneRepository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsMilestoneForm form)
        {
            var data = await _milestoneRepository.FindAsync(form.Id);

            if (data == null) return BaseErrType.DataNotFound;
            if (data.PmsProjectId != projectId) return BaseErrType.DataNotFound;

            _mapper.Map(form, data);
            return await ResultAsync(() => _milestoneRepository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var data = await _milestoneRepository.GetListAsync(w => ids.Contains(w.Id));
            return await ResultAsync(() => _milestoneRepository.DeleteRangeAsync(data));
        }
    }
}
