using AutoMapper;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.Extension;
using OneForAll.Core.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;
using Pms.Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Pms.Application
{
    /// <summary>
    /// 应用服务：项目
    /// </summary>
    public class PmsProjectService : IPmsProjectService
    {
        private readonly IMapper _mapper;
        private readonly IPmsProjectManager _manager;
        private readonly IPmsStatisticsManager _statisticsManager;
        public PmsProjectService(
            IMapper mapper,
            IPmsProjectManager manager
            )
        {
            _mapper = mapper;
            _manager = manager;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="scope">项目范围</param>
        /// <param name="name">项目名称</param>
        /// <returns>项目列表</returns>
        public async Task<IEnumerable<PmsProjectDto>> GetListAsync(PmsProjectVisitEnum scope, string name)
        {
            var data = await _manager.GetListAsync(scope, name);
            return _mapper.Map<IEnumerable<PmsProject>, IEnumerable<PmsProjectDto>>(data);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(PmsProjectForm form)
        {
            return await _manager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(PmsProjectForm form)
        {
            var editable = await _manager.CheckProjectAuthorization(form.Id);
            if (editable)
            {
                return await _manager.UpdateAsync(form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id)
        {
            return await _manager.DeleteAsync(id);
        }

        /// <summary>
        /// 设为星标项目
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SetToStarAsync(Guid id)
        {
            var editable = await _manager.CheckProjectAuthorization(id);
            if (editable)
            {
                return await _manager.SetToStarAsync(id);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 获取项目成员任务统计
        /// </summary>
        /// <param name="id">项目id</param>
        /// <returns>任务统计列表</returns>
        public async Task<IEnumerable<PmsMemberTaskStatisticsDto>> GetMemberStatisticsAsync(Guid id)
        {
            return default;
            var data = await _statisticsManager.GetMemberTasksAsync(id);
            return _mapper.Map<IEnumerable<PmsMemberTaskStatistics>, IEnumerable<PmsMemberTaskStatisticsDto>>(data);
        }
    }
}