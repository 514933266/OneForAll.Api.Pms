using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using Org.BouncyCastle.Crypto;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application
{
    /// <summary>
    /// 代码结构
    /// </summary>
    public class PmsCodeStructureService : IPmsCodeStructureService
    {
        private readonly IMapper _mapper;
        private readonly ISysCodeStructureManager _manager;
        private readonly IPmsProjectManager _projectManager;
        private readonly IPmsCodeDenerationRecordRepository _recordRepository;

        public PmsCodeStructureService(
            IMapper mapper,
            ISysCodeStructureManager manager,
            IPmsProjectManager projectManager,
            IPmsCodeDenerationRecordRepository recordRepository)
        {
            _mapper = mapper;
            _manager = manager;
            _projectManager = projectManager;
            _recordRepository = recordRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<PmsCodeStructureTreeDto>> GetListAsync(Guid projectId, string key)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetListAsync(projectId, key);
                var tree = _mapper.Map<IEnumerable<PmsCodeStructure>, IEnumerable<PmsCodeStructureTreeDto>>(data);
                return tree.ToTree<PmsCodeStructureTreeDto, Guid>();
            }
            return new List<PmsCodeStructureTreeDto>();

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="entity">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsCodeStructureForm entity)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.AddAsync(projectId, entity);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="entity">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsCodeStructureForm entity)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateAsync(projectId, entity);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">菜单id</param>
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

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CreateAsync(Guid projectId, PmsCodeStructureCreateForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.CreateAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 获取代码生成记录
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>记录列表</returns>
        public async Task<IEnumerable<PmsCodeDenerationRecordDto>> GetListRecordAsync(Guid projectId)
        {
            var data = await _recordRepository.GetListAsync(w => w.PmsProjectId == projectId);
            return _mapper.Map<IEnumerable<PmsCodeDenerationRecord>, IEnumerable<PmsCodeDenerationRecordDto>>(data);
        }
    }
}
