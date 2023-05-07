using AutoMapper;
using OneForAll.Core.Upload;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Pms.Application
{
    /// <summary>
    /// 需求
    /// </summary>
    public class PmsRequirementService : IPmsRequirementService
    {
        private readonly IMapper _mapper;
        private readonly IPmsRequirementManager _manager;
        private readonly IPmsRequirementRecordManager _recordManager;
        public PmsRequirementService(
            IMapper mapper,
            IPmsRequirementManager manager,
            IPmsRequirementRecordManager recordManager
            )
        {
            _mapper = mapper;
            _manager = manager;
            _recordManager = recordManager;
        }

        /// <summary>
        /// 获取需求分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>需求分页</returns>
        public async Task<PageList<PmsRequirementDto>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            var data = await _manager.GetPageAsync(projectId, pageIndex, pageSize, key);
            var items = _mapper.Map<IEnumerable<PmsRequirement>, IEnumerable<PmsRequirementDto>>(data.Items);
            return new PageList<PmsRequirementDto>(data.Total, data.PageIndex, data.PageSize, items);
        }

        /// <summary>
        /// 获取需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <returns></returns>
        public async Task<PmsRequirementDto> GetAsync(Guid projectId, Guid id)
        {
            var data = await _manager.GetAsync(projectId, id);
            return _mapper.Map<PmsRequirement, PmsRequirementDto>(data);
        }

        /// <summary>
        /// 添加需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">需求表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsRequirementForm form)
        {
            return await _manager.AddAsync(projectId, form);
        }

        /// <summary>
        /// 修改需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">需求表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateRequirementAsync(Guid projectId, PmsRequirementForm form)
        {
            return await _manager.UpdateAsync(projectId, form);
        }

        /// <summary>
        /// 删除需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="requirementIds">需求id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> requirementIds)
        {
            return await _manager.DeleteAsync(projectId, requirementIds);
        }

        /// <summary>
        /// 上传需求图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="filename">实体名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid projectId, string filename, Stream file)
        {
            return await _manager.UploadImageAsync(projectId, filename, file);
        }

        /// <summary>
        /// 获取需求历史记录列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="requirementId">需求id</param>
        /// <returns>结果</returns>
        public async Task<IEnumerable<PmsRequirementRecordDto>> GetListRecordAsync(Guid projectId, Guid requirementId)
        {
            var data = await _recordManager.GetListAsync(requirementId);
            return _mapper.Map<IEnumerable<PmsRequirementRecord>, IEnumerable<PmsRequirementRecordDto>>(data);
        }

        /// <summary>
        /// 删除需求历史记录
        /// </summary>
        /// <param name="projectId">实体id</param>
        /// <param name="requirementId">需求id</param>
        /// <param name="recordId">记录id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteRecordAsync(Guid projectId, Guid requirementId, Guid recordId)
        {
            return await _recordManager.DeleteAsync(requirementId, recordId);
        }
    }
}
