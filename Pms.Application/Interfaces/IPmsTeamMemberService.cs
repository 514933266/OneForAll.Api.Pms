using Pms.Application.Dtos;
using Pms.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;

namespace Pms.Application.Interfaces
{
    /// <summary>
    /// 应用服务：用户
    /// </summary>
    public interface IPmsTeamMemberService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>用户列表</returns>
        Task<IEnumerable<PmsTeamMemberDto>> GetListAsync();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(PmsMemberForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(PmsMemberForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 绑定系统账号
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> BindAccountAsync(Guid id, PmsMemberBindAccountForm form);
    }
}
