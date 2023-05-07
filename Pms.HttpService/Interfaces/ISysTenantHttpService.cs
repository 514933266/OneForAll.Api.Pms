using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pms.HttpService.Models;

namespace Pms.HttpService.Interfaces
{
    /// <summary>
    /// Http服务：租户
    /// </summary>
    public interface ISysTenantHttpService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns>返回消息</returns>
        Task<BaseMessage> AddAsync(SysTenantForm postData);

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns>返回消息</returns>
        Task<BaseMessage> UpdateAsync(SysTenantForm postData);
    }
}
