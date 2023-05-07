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
using Pms.Domain.Enums;
using System.Security.Cryptography.X509Certificates;

namespace Pms.Host.Controllers
{
    /// <summary>
    /// 项目
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Action = ConstPermission.EnterView)]
    public class PmsProjectsController : BaseController
    {
        private readonly IPmsProjectService _projectService;
        public PmsProjectsController(IPmsProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="scope">项目范围</param>
        /// <param name="name">项目名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PmsProjectDto>> GetListAsync([FromQuery] PmsProjectVisitEnum scope, [FromQuery] string name = default)
        {
            return await _projectService.GetListAsync(scope, name);
        }

        /// <summary>
        /// 添加
        /// </summary>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromBody] PmsProjectForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _projectService.AddAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("项目名称已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("项目信息不存在");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromBody] PmsProjectForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _projectService.UpdateAsync(form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("项目名称已被使用");
                case BaseErrType.DataNotFound: return msg.Fail("项目信息不存在");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<BaseMessage> DeleteAsync(Guid id)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _projectService.DeleteAsync(id);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 标记为星标
        /// </summary>
        [HttpPatch]
        [Route("{id}/Star")]
        public async Task<BaseMessage> SetToStarAsync(Guid id)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _projectService.SetToStarAsync(id);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("设置星标项目成功");
                case BaseErrType.DataNotFound: return msg.Success("项目不存在");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                default: return msg.Fail("设置星标项目失败");
            }
        }

        /// <summary>
        /// 获取项目成员任务统计
        /// </summary>
        [HttpGet]
        [Route("{id}/Members/TaskStatistic")]
        public async Task<IEnumerable<PmsMemberTaskStatisticsDto>> GetMemberStatisticsAsync(Guid id)
        {
            return await _projectService.GetMemberStatisticsAsync(id);
        }
    }
}
