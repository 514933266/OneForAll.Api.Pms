using System;
using System.Collections.Generic;
using OneForAll.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pms.Application.Dtos;
using System.Threading.Tasks;
using Pms.Host.Models;
using Pms.Domain.Models;
using Pms.Application.Interfaces;
using Pms.Public.Models;
using Pms.Host.Filters;

namespace Pms.Host.Controllers
{
    /// <summary>
    /// 代码结构
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsCodeStructuresController : BaseController
    {
        private readonly IPmsCodeStructureService _service;
        public PmsCodeStructuresController(IPmsCodeStructureService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取代码结构树
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key"></param>
        /// <returns>代码结构树</returns>
        [HttpGet]
        public async Task<IEnumerable<PmsCodeStructureTreeDto>> GetListAsync([FromQuery] Guid projectId, [FromQuery] string key = "")
        {
            return await _service.GetListAsync(projectId, key);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="entity">代码结构表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] PmsCodeStructureForm entity)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataNotFound: return msg.Fail("找不到上级代码结构");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="entity">代码结构表单</param>
        /// <returns>结果</returns>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromQuery] Guid projectId, [FromBody] PmsCodeStructureForm entity)
        {
            var msg = new BaseMessage() { Status = false };
            msg.ErrType = await _service.UpdateAsync(projectId, entity);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataNotFound: return msg.Fail("找不到上级代码结构");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">代码结构id</param>
        /// <returns>消息</returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        public async Task<BaseMessage> DeleteAsync([FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> ids)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(projectId, ids);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataExist: return msg.Fail("当前代码结构存在子级");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("Current/Files")]
        public async Task<BaseMessage> CreateAsync([FromQuery] Guid projectId, [FromBody] PmsCodeStructureCreateForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.CreateAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("代码生成成功");
                case BaseErrType.DataExist: return msg.Fail("生成太频繁，请1分钟后操作");
                case BaseErrType.DataEmpty: return msg.Fail("请先添加生成模板");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("代码生成失败");
            }
        }


        /// <summary>
        /// 获取代码生成记录
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>记录列表</returns>
        [HttpGet]
        [Route("Current/Records")]
        public async Task<IEnumerable<PmsCodeDenerationRecordDto>> GetListRecordAsync([FromQuery] Guid projectId)
        {
            return await _service.GetListRecordAsync(projectId);
        }
    }
}
