using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pms.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using Pms.Domain.Models;
using OneForAll.Core.Upload;
using Microsoft.AspNetCore.Authorization;
using Pms.Application.Interfaces;
using Pms.HttpService.Models;
using Pms.Host.Filters;
using Pms.Public.Models;
using Pms.Application;

namespace Pms.Host.Controllers
{
    /// <summary>
    /// 项目成员
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsMembersController : BaseController
    {
        private readonly IPmsMemberService _service;
        public PmsMembersController(IPmsMemberService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>结果</returns>
        [HttpGet]
        public async Task<IEnumerable<PmsProjectMemberDto>> GetListAsync([FromQuery] Guid projectId)
        {
            return await _service.GetListAsync(projectId);
        }

        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="userIds">用户id集合</param>
        /// <returns>结果</returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> userIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, userIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加项目成员成功");
                case BaseErrType.DataNotFound: return msg.Success("信息不存在");
                case BaseErrType.NotAllow: return msg.Fail("不允许操作");
                default: return msg.Fail("添加项目成员失败");
            }
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">用户id</param>
        /// <returns>结果</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<BaseMessage> DeleteAsync(Guid id, [FromQuery] Guid projectId)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(projectId, id);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除项目成员成功");
                case BaseErrType.DataNotFound: return msg.Fail("成员信息不存在");
                case BaseErrType.NotAllow: return msg.Fail("不允许操作");
                default: return msg.Fail("删除项目成员失败");
            }
        }
    }
}
