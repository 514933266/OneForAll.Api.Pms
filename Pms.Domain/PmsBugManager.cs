using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using OneForAll.Core.Upload;
using OneForAll.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Pms.Public.Models;
using Microsoft.AspNetCore.Http;
using Pms.Domain.Aggregates;

namespace Pms.Domain
{
    /// <summary>
    /// 项目Bug
    /// </summary>
    public class PmsBugManager : PmsBaseManager, IPmsBugManager
    {
        // 文件存储路径
        private readonly string UPLOAD_PATH = "upload/projects/{0}/bugs/{1}/";
        // 虚拟路径：根据Startup配置设置,返回给前端访问资源
        private readonly string VIRTUAL_PATH = "resources/projects/{0}/bugs/{1}/";

        private readonly IUploader _uploader;
        private readonly IPmsBugRepository _repository;
        public PmsBugManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUploader uploader,
            IPmsBugRepository repository) : base(mapper, httpContextAccessor)
        {
            _uploader = uploader;
            _repository = repository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>Bug分页</returns>
        public async Task<PageList<PmsBugAggregate>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            return await _repository.GetPageAsync(projectId, pageIndex, pageSize, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsBugForm form)
        {
            var data = _mapper.Map<PmsBugForm, PmsBug>(form);

            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            data.PmsProjectId = projectId;
            data.UpdateTime = DateTime.Now;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsBugForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataError;

            _mapper.Map(form, data);
            data.UpdateTime = DateTime.Now;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="projectId">仓库id</param>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid projectId, Guid id, string filename, Stream file)
        {
            var maxSize = 2 * 1024 * 1024;
            var bug = await _repository.FindAsync(id);
            if (bug == null)
            {
                bug = new PmsBug();
                bug.Id = id;
                bug.PmsProjectId = projectId;
            }
            if (new ValidateImageType().Validate(filename, file))
            {
                var result = await _uploader.WriteAsync(file, UPLOAD_PATH.Fmt(bug.PmsProjectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename, maxSize);
                // 设置返回虚拟路径
                if (result.State.Equals(UploadEnum.Success))
                {
                    result.Url = Path.Combine(VIRTUAL_PATH.Fmt(bug.PmsProjectId, DateTime.Now.Date.ToString("yyyyMMdd")), filename);
                }
                return result;
            }
            return new UploadResult();
        }
    }
}
