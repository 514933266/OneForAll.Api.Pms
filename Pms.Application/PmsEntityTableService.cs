using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Extension;
using Org.BouncyCastle.Asn1.X509;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using Pms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application
{
    /// <summary>
    /// 表实体
    /// </summary>
    public class PmsEntityTableService : IPmsEntityTableService
    {
        private readonly IMapper _mapper;
        private readonly IPmsEntityTableManager _manager;
        private readonly IPmsProjectManager _projectManager;
        private readonly IPmsDbConnectStringManager _connManager;

        private readonly IPmsEntityTableRepository _repository;
        private readonly IPmsEntityTableContactRepository _contactRepository;

        public PmsEntityTableService(IMapper mapper,
            IPmsEntityTableManager manager,
            IPmsProjectManager projectManager,
            IPmsDbConnectStringManager connManager,
            IPmsEntityTableRepository repository,
            IPmsEntityTableContactRepository contactRepository)
        {
            _mapper = mapper;
            _manager = manager;
            _projectManager = projectManager;
            _connManager = connManager;
            _repository = repository;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// 获取表实体列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>表实体列表</returns>
        public async Task<IEnumerable<PmsEntityTableDto>> GetListAsync(Guid projectId, string key)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetListAsync(projectId, key);
                return _mapper.Map<IEnumerable<PmsEntityTable>, IEnumerable<PmsEntityTableDto>>(data);
            }
            return new List<PmsEntityTableDto>();
        }

        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表实体实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsEntityTableForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                if (form.Name != "自动创建")
                {
                    return await _manager.AddAsync(projectId, form);
                }
                else
                {
                    var conn = await _connManager.GetAsync(projectId);
                    if (conn != null && !conn.ConnectString.IsNullOrEmpty())
                    {
                        return await _manager.CreateAsync(projectId, conn.ConnectString);
                    }
                    return BaseErrType.DataError;
                }
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 修改表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsEntityTableForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">表id</param>
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
        /// 获取表字段列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <returns>表字段列表</returns>
        public async Task<IEnumerable<PmsEntityFieldVo>> GetListFieldAsync(Guid projectId, Guid id)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _repository.FindAsync(id);
                if (data != null && !data.FiledJson.IsNullOrEmpty())
                    return data.FiledJson.FromJson<IEnumerable<PmsEntityFieldVo>>();
            }
            return new List<PmsEntityFieldVo>();
        }

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateFieldAsync(Guid projectId, Guid id, PmsEntityTableUpdateFieldForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateFieldAsync(projectId, id, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 获取表关联列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <returns>表字段列表</returns>
        public async Task<IEnumerable<PmsEntityTableDto>> GetListContactAsync(Guid projectId, Guid id)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var contacts = await _contactRepository.GetListAsync(projectId, id);
                var ids = contacts.Where(w => w.SourceTableId == id).Select(s => s.TargetTableId).Union(contacts.Where(w => w.TargetTableId == id).Select(s => s.SourceTableId)).ToList();
                if (ids.Any())
                {
                    var data = await _repository.GetListAsync(ids);
                    return _mapper.Map<IEnumerable<PmsEntityTable>, IEnumerable<PmsEntityTableDto>>(data);
                }
            }
            return new List<PmsEntityTableDto>();
        }

        /// <summary>
        /// 修改关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="targetIds">关联表id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateContactAsync(Guid projectId, Guid id, IEnumerable<Guid> targetIds)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateContactAsync(projectId, id, targetIds);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="contactIds">关联表id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteContactAsync(Guid projectId, Guid id, IEnumerable<Guid> contactIds)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.DeleteContactAsync(projectId, id, contactIds);
            }
            return BaseErrType.NotAllow;
        }
    }
}
