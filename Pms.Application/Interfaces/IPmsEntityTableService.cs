using OneForAll.Core;
using OneForAll.EFCore;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using Pms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Interfaces
{
    /// <summary>
    /// 表实体
    /// </summary>
    public interface IPmsEntityTableService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsEntityTableDto>> GetListAsync(Guid projectId, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsEntityTableForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsEntityTableForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">表id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);

        /// <summary>
        /// 获取表字段列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <returns>表字段列表</returns>
        Task<IEnumerable<PmsEntityFieldVo>> GetListFieldAsync(Guid projectId, Guid id);

        /// <summary>
        /// 修改字段
        /// </summary>
        ///  <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateFieldAsync(Guid projectId, Guid id, PmsEntityTableUpdateFieldForm form);

        /// <summary>
        /// 获取表关联列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <returns>表字段列表</returns>
        Task<IEnumerable<PmsEntityTableDto>> GetListContactAsync(Guid projectId, Guid id);

        /// <summary>
        /// 修改关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="targetIds">关联表id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateContactAsync(Guid projectId, Guid id, IEnumerable<Guid> targetIds);

        /// <summary>
        /// 删除关联
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">表id</param>
        /// <param name="contactIds">关联表id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteContactAsync(Guid projectId, Guid id, IEnumerable<Guid> contactIds);
    }
}
