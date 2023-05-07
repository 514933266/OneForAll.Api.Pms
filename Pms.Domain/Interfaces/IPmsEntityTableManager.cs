using OneForAll.Core;
using OneForAll.EFCore;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 表实体
    /// </summary>
    public interface IPmsEntityTableManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>Bug分页</returns>
        Task<IEnumerable<PmsEntityTable>> GetListAsync(Guid projectId, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsEntityTableForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsEntityTableForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);

        /// <summary>
        /// 修改字段
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateFieldAsync(Guid projectId, Guid id, PmsEntityTableUpdateFieldForm form);

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

        /// <summary>
        /// 自动根据连接字符串生成
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="connectString">连接字符串</param>
        /// <returns>结果</returns>
        Task<BaseErrType> CreateAsync(Guid projectId, string connectString);
    }
}
