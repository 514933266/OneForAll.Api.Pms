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
    /// 项目需求
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsRequirementsController : BaseController
    {
        private readonly IPmsRequirementService _service;
        public PmsRequirementsController(IPmsRequirementService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取需求分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        public async Task<PageList<PmsRequirementDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] Guid projectId, [FromQuery] string key)
        {
            return await _service.GetPageAsync(projectId, pageIndex, pageSize, key);
        }

        /// <summary>
        /// 获取需求
        /// </summary>
        /// <param name="id">需求id</param>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<PmsRequirementDto> GetAsync(Guid id, [FromQuery] Guid projectId)
        {
            return await _service.GetAsync(projectId, id);
        }

        /// <summary>
        /// 添加需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] PmsRequirementForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("需求已存在");
                case BaseErrType.DataNotFound: return msg.Fail("项目信息不存在");
                case BaseErrType.NotAllow: return msg.Fail("不允许操作");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromQuery] Guid projectId, [FromBody] PmsRequirementForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateRequirementAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("需求已存在");
                case BaseErrType.DataNotFound: return msg.Fail("信息不存在");
                case BaseErrType.NotAllow: return msg.Fail("不允许操作");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="requiementIds">需求id</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        public async Task<BaseMessage> DeleteAsync([FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> requiementIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(projectId, requiementIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Success("信息不存在");
                case BaseErrType.NotAllow: return msg.Fail("不允许操作");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 上传需求图片
        /// </summary>
        [HttpPost]
        [Route("{id}/Images")]
        public async Task<BaseMessage> UploadImageAsync(Guid id, [FromQuery] Guid projectId, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var callbacks = await _service.UploadImageAsync(projectId, file.FileName, file.OpenReadStream());

                msg.Data = new { Id = id, Result = callbacks };

                switch (callbacks.State)
                {
                    case UploadEnum.Success: return msg.Success("上传成功");
                    case UploadEnum.Overflow: return msg.Fail("文件超出限制大小2MB");
                    case UploadEnum.Error: return msg.Fail("上传过程中发生未知错误");
                    case UploadEnum.TypeError: return msg.Fail("不支持上传该文件格式");
                    default: return msg.Fail("上传失败");
                }
            }
            return msg.Fail("上传失败，请选择文件");
        }

        /// <summary>
        /// 获取历史记录列表
        /// </summary>
        /// <param name="id">需求id</param>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/Records")]
        public async Task<IEnumerable<PmsRequirementRecordDto>> GetListRecordAsync(Guid id, [FromQuery] Guid projectId)
        {
            return await _service.GetListRecordAsync(projectId, id);
        }

        /// <summary>
        /// 删除历史记录
        /// </summary>
        /// <param name="id">需求id</param>
        /// <param name="recordId">记录id</param>
        /// <param name="projectId">项目id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}/Records/{recordId}")]
        public async Task<BaseErrType> DeleteRecordAsync(Guid id, Guid recordId, [FromQuery] Guid projectId)
        {
            return await _service.DeleteRecordAsync(projectId, id, recordId);
        }
    }
}
