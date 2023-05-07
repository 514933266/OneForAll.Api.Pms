using Pms.HttpService.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.HttpService.Interfaces
{
    /// <summary>
    /// Http服务：OA人员档案
    /// </summary>
    public interface IOAPersonHttpService
    {
        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>人员</returns>
        Task<OAPersonBasicInfo> GetAsync(Guid id);

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonBasicInfo>> GetListAsync();

        /// <summary>
        /// 获取在职人员列表（基础信息）
        /// </summary>
        /// <param name="ids">人员id集合</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonBasicInfo>> GetInserviceListAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OAPersonBasicInfo>> GetInserviceListAsync();

        /// <summary>
        /// 设置离职
        /// </summary>
        /// <param name="id">人员id</param>
        /// <returns>结果</returns>
        Task<BaseMessage> SetLeaveAsync(Guid id);
    }
}
