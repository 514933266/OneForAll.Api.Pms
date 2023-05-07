using OneForAll.Core.Upload;
using OneForAll.Core;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Interfaces
{
    /// <summary>
    /// 项目需求
    /// </summary>
    public interface IPmsRequirementService
    {
        /// <summary>
        /// 获取需求分页
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <returns>需求分页</returns>
        Task<PageList<PmsRequirementDto>> GetPageAsync(Guid projectId, int pageIndex, int pageSize, string key);

        /// <summary>
        /// 获取需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <returns></returns>
        Task<PmsRequirementDto> GetAsync(Guid projectId, Guid id);

        /// <summary>
        /// 添加需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">需求表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsRequirementForm form);

        /// <summary>
        /// 修改需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">需求表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateRequirementAsync(Guid projectId, PmsRequirementForm form);

        /// <summary>
        /// 删除需求
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="requirementIds">需求id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> requirementIds);

        /// <summary>
        /// 上传需求图片
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="filename">实体名</param>
        /// <param name="file">文件流</param>
        /// <returns>上传结果</returns>
        Task<IUploadResult> UploadImageAsync(Guid projectId, string filename, Stream file);

        /// <summary>
        /// 获取需求历史记录列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="requirementId">需求id</param>
        /// <returns>结果</returns>
        Task<IEnumerable<PmsRequirementRecordDto>> GetListRecordAsync(Guid projectId, Guid requirementId);

        /// <summary>
        /// 删除需求历史记录
        /// </summary>
        /// <param name="projectId">实体id</param>
        /// <param name="requirementId">需求id</param>
        /// <param name="recordId">记录id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteRecordAsync(Guid projectId, Guid requirementId, Guid recordId);
    }
}
