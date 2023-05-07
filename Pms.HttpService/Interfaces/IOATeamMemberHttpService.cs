using Pms.HttpService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.HttpService.Interfaces
{
    /// <summary>
    /// Http服务：团队成员
    /// </summary>
    public interface IOATeamMemberHttpService
    {
        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OATeamMember>> GetListAsync();

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OATeamMember>> GetListAsync(string key);

        /// <summary>
        /// 获取列表（基础信息）
        /// </summary>
        /// <param name="endDate">结束时间</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OATeamMember>> GetListAsync(DateTime endDate);
    }
}
