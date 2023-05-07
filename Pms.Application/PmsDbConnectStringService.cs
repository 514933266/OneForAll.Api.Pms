using AutoMapper;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
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
    /// 数据库连接字符串
    /// </summary>
    public class PmsDbConnectStringService : IPmsDbConnectStringService
    {
        private readonly IMapper _mapper;
        private readonly IPmsDbConnectStringManager _manager;
        private readonly IPmsProjectManager _projectManager;
        public PmsDbConnectStringService(IMapper mapper,
            IPmsDbConnectStringManager manager,
            IPmsProjectManager projectManager)
        {
            _mapper = mapper;
            _manager = manager;
            _projectManager = projectManager;
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>字符串</returns>
        public async Task<PmsDbConnectStringDto> GetAsync(Guid projectId)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetAsync(projectId);
                return _mapper.Map<PmsDbConnectString, PmsDbConnectStringDto>(data);
            }
            return null;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsDbConnectStringForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                if (form.Id == Guid.Empty)
                {
                    return await _manager.AddAsync(projectId, form);
                }
                else
                {
                    return await _manager.UpdateAsync(projectId, form);
                }
            }
            return BaseErrType.NotAllow;
        }
    }
}
