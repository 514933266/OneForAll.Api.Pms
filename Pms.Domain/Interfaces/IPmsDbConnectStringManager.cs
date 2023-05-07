using OneForAll.Core;
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
    /// 数据库连接字符串
    /// </summary>
    public interface IPmsDbConnectStringManager
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>字符串</returns>
        Task<PmsDbConnectString> GetAsync(Guid projectId);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> AddAsync(Guid projectId, PmsDbConnectStringForm form);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        Task<BaseErrType> UpdateAsync(Guid projectId, PmsDbConnectStringForm form);
    }
}
