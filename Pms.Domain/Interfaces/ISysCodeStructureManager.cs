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
    /// 代码结构
    /// </summary>
    public interface ISysCodeStructureManager
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        Task<IEnumerable<PmsCodeStructure>> GetListAsync(Guid projectId, string key);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="entity">菜单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsCodeStructureForm entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="entity">菜单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsCodeStructureForm entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="ids">菜单id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid projectId, IEnumerable<Guid> ids);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        Task<BaseErrType> CreateAsync(Guid projectId, PmsCodeStructureCreateForm form);
    }
}
