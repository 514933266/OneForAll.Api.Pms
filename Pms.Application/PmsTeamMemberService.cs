using AutoMapper;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;

namespace Pms.Application
{
    /// <summary>
    /// 应用服务：用户
    /// </summary>
    public class PmsTeamMemberService : IPmsTeamMemberService
    {
        private readonly IMapper _mapper;
        private readonly IPmsTeamMemberManager _memberManager;
        public PmsTeamMemberService(
            IPmsTeamMemberManager memberManager,
            IMapper mapper)
        {
            _mapper = mapper;
            _memberManager = memberManager;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>用户列表</returns>
        public async Task<IEnumerable<PmsTeamMemberDto>> GetListAsync()
        {
            var members = await _memberManager.GetListAsync();
            return _mapper.Map<IEnumerable<PmsMember>, IEnumerable<PmsTeamMemberDto>>(members);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(PmsMemberForm form)
        {
            return await _memberManager.AddAsync(form);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(PmsMemberForm form)
        {
            return await _memberManager.UpdateAsync(form);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _memberManager.DeleteAsync(ids);
        }

        /// <summary>
        /// 绑定系统账号
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> BindAccountAsync(Guid id, PmsMemberBindAccountForm form)
        {
            return await _memberManager.BindAccountAsync(id, form);
        }
    }
}
