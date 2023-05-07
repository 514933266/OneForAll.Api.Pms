using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Security;
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
    /// 团队成员
    /// </summary>
    public class PmsTeamMemberManager : PmsBaseManager, IPmsTeamMemberManager
    {
        private readonly IPmsMemberRepository _repository;
        public PmsTeamMemberManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsMemberRepository repository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<PmsMember>> GetListAsync()
        {
            return await _repository.GetListAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(PmsMemberForm form)
        {
            var data = _mapper.Map<PmsMemberForm, PmsMember>(form);
            data.Id = Guid.NewGuid();
            data.SysTenantId = LoginUser.TenantId;
            return await ResultAsync(() => _repository.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(PmsMemberForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataError;

            _mapper.Map(form, data);
            return await ResultAsync(() => _repository.SaveChangesAsync());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var data = await _repository.GetListAsync(w => ids.Contains(w.Id));
            if (!data.Any())
                return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 绑定系统账号
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> BindAccountAsync(Guid id, PmsMemberBindAccountForm form)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataError;

            _mapper.Map(form, data);
            return await ResultAsync(() => _repository.SaveChangesAsync());
        }
    }
}
