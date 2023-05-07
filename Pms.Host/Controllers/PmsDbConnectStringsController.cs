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

namespace Pms.Host.Controllers
{
    /// <summary>
    /// 项目Bug
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsDbConnectStringsController : BaseController
    {
        private readonly IPmsDbConnectStringService _service;
        public PmsDbConnectStringsController(IPmsDbConnectStringService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PmsDbConnectStringDto> GetAsync([FromQuery] Guid projectId)
        {
            return await _service.GetAsync(projectId);
        }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] PmsDbConnectStringForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("设置成功");
                default: return msg.Fail("设置失败");
            }
        }
    }
}
