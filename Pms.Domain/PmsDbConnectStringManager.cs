using AutoMapper;
using Microsoft.AspNetCore.Http;
using OneForAll.Core;
using OneForAll.Core.Upload;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class PmsDbConnectStringManager : PmsBaseManager, IPmsDbConnectStringManager
    {
        private readonly IPmsDbConnectStringRepository _repository;
        public PmsDbConnectStringManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsDbConnectStringRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>字符串</returns>
        public async Task<PmsDbConnectString> GetAsync(Guid projectId)
        {
            return await _repository.GetAsync(w => w.PmsProjectId == projectId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, PmsDbConnectStringForm form)
        {
            var data = _mapper.Map<PmsDbConnectStringForm, PmsDbConnectString>(form);

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
        public async Task<BaseErrType> UpdateAsync(Guid projectId, PmsDbConnectStringForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;
            if (data.PmsProjectId != projectId)
                return BaseErrType.DataError;

            _mapper.Map(form, data);
            data.UpdateTime = DateTime.Now;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }
    }
}
