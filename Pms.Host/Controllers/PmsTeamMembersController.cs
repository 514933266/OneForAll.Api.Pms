using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pms.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using Microsoft.AspNetCore.Authorization;
using Pms.Domain.Models;
using Pms.Application.Interfaces;
using Pms.HttpService.Models;
using Pms.Public.Models;
using Pms.Host.Filters;

namespace Pms.Host.Controllers
{
    /// <summary>
    /// 项目成员
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Action = ConstPermission.EnterView)]
    public class PmsTeamMembersController : BaseController
    {
        private readonly IPmsTeamMemberService _service;
        public PmsTeamMembersController(IPmsTeamMemberService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<PmsTeamMemberDto>> GetListAsync()
        {
            return await _service.GetListAsync();
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromBody]PmsMemberForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success:   return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("用户名已被使用");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromBody]PmsMemberForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success:   return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("用户名已被使用");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        public async Task<BaseMessage> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataEmpty: return msg.Fail("没有可以删除的数据");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 绑定系统账号
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/Account")]
        public async Task<BaseMessage> BindAccountAsync(Guid id, [FromBody] PmsMemberBindAccountForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.BindAccountAsync(id, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("绑定成功");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                default: return msg.Fail("绑定失败");
            }
        }
    }
}
