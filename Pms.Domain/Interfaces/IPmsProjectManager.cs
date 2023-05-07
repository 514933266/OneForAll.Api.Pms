using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pms.Domain.Enums;

namespace Pms.Domain.Interfaces
{
    /// <summary>
    /// 项目
    /// </summary>
    public interface IPmsProjectManager
    {
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="scope">项目范围</param>
        /// <param name="name">项目名称</param>
        /// <returns>项目列表</returns>
        Task<IEnumerable<PmsProject>> GetListAsync(PmsProjectVisitEnum scope, string name);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(PmsProjectForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(PmsProjectForm form);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> DeleteAsync(Guid id);

        /// <summary>
        /// 设为星标项目
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        Task<BaseErrType> SetToStarAsync(Guid id);

        /// <summary>
        /// 检查项目授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CheckProjectAuthorization(Guid id);
    }
}
