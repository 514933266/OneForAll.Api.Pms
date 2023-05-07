using AutoMapper;
using OneForAll.Core.Extension;
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
using Pms.Domain.Aggregates;
using Org.BouncyCastle.Crypto;

namespace Pms.Application
{
    /// <summary>
    /// 项目bug
    /// </summary>
    public class PmsBugService : IPmsBugService
    {
        private readonly IMapper _mapper;
        private readonly IPmsBugManager _manager;
        private readonly IPmsProjectManager _projectManager;
        public PmsBugService(IMapper mapper,
            IPmsBugManager manager,
            IPmsProjectManager projectManager)
        {
            _mapper = mapper;
            _manager = manager;
            _projectManager = projectManager;
        }

        /// <summary>
        /// 获取Bug分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>Bug分页</returns>
        public async Task<PageList<PmsBugDto>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                var data = await _manager.GetPageAsync(projectId, pageIndex, pageSize, key);
                var items = _mapper.Map<IEnumerable<PmsBugAggregate>, IEnumerable<PmsBugDto>>(data.Items);
                return new PageList<PmsBugDto>(data.Total, data.PageIndex, data.PageSize, items);
            }
            return new PageList<PmsBugDto>(0, pageIndex, pageSize, new List<PmsBugDto>());
        }

        /// <summary>
        /// 添加Bug
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">Bug实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsBugForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.AddAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 修改Bug
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">Bug实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsBugForm form)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UpdateAsync(projectId, form);
            }
            return BaseErrType.NotAllow;
        }

        /// <summary>
        /// 删除Bug
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">Bug id</param>
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
        /// 上传Bug图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">Bug Id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid projectId, Guid id, string filename, Stream file)
        {
            var editable = await _projectManager.CheckProjectAuthorization(projectId);
            if (editable)
            {
                return await _manager.UploadImageAsync(projectId, id, filename, file);
            }
            return default;
        }
    }
}
