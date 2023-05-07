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
using Microsoft.CodeAnalysis;
using Pms.Domain.ValueObjects;
using Pms.Domain.AggregateRoots;
using Microsoft.Build.Evaluation;

namespace Pms.Host.Controllers
{
    /// <summary>
    /// 表实体
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsEntityTablesController : BaseController
    {
        private readonly IPmsEntityTableService _service;
        public PmsEntityTablesController(IPmsEntityTableService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取表实体列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<PmsEntityTableDto>> GetListAsync([FromQuery] Guid projectId, [FromQuery] string key)
        {
            return await _service.GetListAsync(projectId, key);
        }

        /// <summary>
        /// 添加表实体
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] PmsEntityTableForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataNotFound: return msg.Fail("表不存在");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                case BaseErrType.DataError: return msg.Fail("数据异常");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改表实体
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromQuery] Guid projectId, [FromBody] PmsEntityTableForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataNotFound: return msg.Fail("表不存在");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除表实体
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="bugIds">表实体id</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        public async Task<BaseMessage> DeleteAsync([FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> bugIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(projectId, bugIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Success("表不存在");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">表id</param>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/FieldJson")]
        public async Task<IEnumerable<PmsEntityFieldVo>> GetListFieldAsync(Guid id, [FromQuery] Guid projectId)
        {
            return await _service.GetListFieldAsync(projectId, id);
        }

        /// <summary>
        /// 修改表字段
        /// </summary>
        /// <param name="id">表id</param>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}/FieldJson")]
        public async Task<BaseMessage> UpdateFieldAsync(Guid id, [FromQuery] Guid projectId, [FromBody] PmsEntityTableUpdateFieldForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateFieldAsync(projectId, id, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("保存字段成功");
                case BaseErrType.DataNotFound: return msg.Fail("表不存在");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("保存字段失败");
            }
        }

        /// <summary>
        /// 获取表关联列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <returns>表字段列表</returns>
        [HttpGet]
        [Route("{id}/Contacts")]
        public async Task<IEnumerable<PmsEntityTableDto>> GetListContactAsync(Guid id, [FromQuery] Guid projectId)
        {
            return await _service.GetListContactAsync(projectId, id);
        }

        /// <summary>
        /// 修改关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="targetIds">关联表id</param>
        /// <returns>结果</returns>
        [HttpPost]
        [Route("{id}/Contacts")]
        public async Task<BaseMessage> UpdateContactAsync(Guid id, [FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> targetIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateContactAsync(projectId, id, targetIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("设置表关联成功");
                case BaseErrType.DataNotFound: return msg.Fail("表不存在");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("设置表关联失败");
            }
        }

        /// <summary>
        /// 修改关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="contactIds">关联表id</param>
        /// <returns>结果</returns>
        [HttpPatch]
        [Route("{id}/Contacts")]
        public async Task<BaseMessage> DeleteContactAsync(Guid id, [FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> contactIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteContactAsync(projectId, id, contactIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("取消表关联成功");
                case BaseErrType.DataNotFound: return msg.Fail("表不存在");
                case BaseErrType.NotAllow: return msg.Fail("项目权限不足");
                default: return msg.Fail("取消表关联失败");
            }
        }
    }
}
