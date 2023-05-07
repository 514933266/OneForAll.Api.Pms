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
    /// 项目任务
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoleType.ADMIN)]
    [CheckPermission(Controller = "PmsProjects", Action = ConstPermission.EnterView)]
    public class PmsTasksController : BaseController
    {
        private readonly IPmsTaskService _service;
        public PmsTasksController(IPmsTaskService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取任务分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{pageIndex}/{pageSize}")]
        public async Task<PageList<PmsTaskDto>> GetPageAsync(int pageIndex, int pageSize, [FromQuery] Guid projectId, [FromQuery] string key)
        {
            return await _service.GetPageAsync(projectId, pageIndex, pageSize, key);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseMessage> AddAsync([FromQuery] Guid projectId, [FromBody] PmsTaskForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.AddAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("添加成功");
                case BaseErrType.DataExist: return msg.Fail("需求名已被使用");
                case BaseErrType.DataEmpty: return msg.Fail("请指定任务人员");
                case BaseErrType.DataNotFound: return msg.Fail("项目信息不存在");
                default: return msg.Fail("添加失败");
            }
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<BaseMessage> UpdateAsync([FromQuery] Guid projectId, [FromBody] PmsTaskForm form)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.UpdateAsync(projectId, form);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("修改成功");
                case BaseErrType.DataExist: return msg.Fail("需求标题已被使用");
                case BaseErrType.DataEmpty: return msg.Fail("请指定任务人员");
                case BaseErrType.DataNotFound: return msg.Fail("信息不存在");
                default: return msg.Fail("修改失败");
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="taskIds">任务id</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("Batch/IsDeleted")]
        public async Task<BaseMessage> DeleteAsync([FromQuery] Guid projectId, [FromBody] IEnumerable<Guid> taskIds)
        {
            var msg = new BaseMessage();
            msg.ErrType = await _service.DeleteAsync(projectId, taskIds);

            switch (msg.ErrType)
            {
                case BaseErrType.Success: return msg.Success("删除成功");
                case BaseErrType.DataNotFound: return msg.Success("信息不存在");
                default: return msg.Fail("删除失败");
            }
        }

        /// <summary>
        /// 上传任务图片
        /// </summary>
        /// <param name="id">任务id</param>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/Images")]
        public async Task<BaseMessage> UploadImageAsync(Guid id, [FromQuery] Guid projectId, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var callbacks = await _service.UploadImageAsync(projectId, id, file.FileName, file.OpenReadStream());

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
        /// 上传任务附件
        /// </summary>
        /// <param name="id">任务id</param>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/Files")]
        public async Task<BaseMessage> UploadFileAsync(Guid id, [FromQuery] Guid projectId, [FromForm] IFormCollection form)
        {
            var msg = new BaseMessage();
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var callbacks = await _service.UploadFileAsync(projectId, id, file.FileName, file.OpenReadStream());

                msg.Data = new { Id = id, Result = callbacks };

                switch (callbacks.State)
                {
                    case UploadEnum.Success: return msg.Success("上传成功");
                    case UploadEnum.Overflow: return msg.Fail("文件超出限制大小20MB");
                    case UploadEnum.Error: return msg.Fail("上传过程中发生未知错误");
                    case UploadEnum.TypeError: return msg.Fail("不支持上传该文件格式");
                    default: return msg.Fail("上传失败");
                }
            }
            return msg.Fail("上传失败，请选择文件");
        }

        #region 任务明细

        /// <summary>
        /// 任务明细列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/Details")]
        public async Task<IEnumerable<PmsTaskDetailDto>> GetListDetailAsync(Guid id, [FromQuery] Guid projectId)
        {
            return await _service.GetListDetailAsync(projectId, id);
        }

        #endregion
    }
}
