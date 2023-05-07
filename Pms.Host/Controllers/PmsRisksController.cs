using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Application.Interfaces;
using Pms.Domain.Models;
using Pms.Host.Filters;
using Pms.Public.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pms.Host.Controllers
{
    /// <summary>
    /// 项目Bug
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsRisksController : BaseController
    {
        private readonly IPmsRiskService _service;
        public PmsRisksController(IPmsRiskService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取风险列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PmsRiskDto>> GetListAsync([FromQuery] Guid projectId)
        {
            return await _service.GetListAsync(projectId);
        }

        /// <summary>
        /// 添加风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] PmsRiskForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("标题已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("项目信息不存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromQuery] Guid projectId, [FromBody] PmsRiskForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("标题已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("信息不存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除风险
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">风险id</param>
        /// <returns>结果</returns>
        [HttpDelete]
        public async Task<BaseMessage> DeleteAsync([FromQuery] Guid projectId, IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(projectId, ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Fail("信息不存在");
                default: return msg.Fail("删除失败");
            }
        }
    }
}
