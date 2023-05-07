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
    /// 项目里程碑
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsMilestonesController : BaseController
    {
        private readonly IPmsMilestoneService _service;
        public PmsMilestonesController(IPmsMilestoneService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取里程碑列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PmsMilestoneDto>> GetListAsync([FromQuery] Guid projectId)
        {
            return await _service.GetListAsync(projectId);
        }

        /// <summary>
        /// 添加里程碑
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] PmsMilestoneForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("需求名已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("项目信息不存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改里程碑
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromQuery] Guid projectId, [FromBody] PmsMilestoneForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("需求标题已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("信息不存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除里程碑
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="milestoneIds">里程碑id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        public async Task<BaseMessage> DeleteAsync([FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> milestoneIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(projectId, milestoneIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Success("信息不存在");
                default: return msg.Fail("删除失败");
            }
        }
    }
}
