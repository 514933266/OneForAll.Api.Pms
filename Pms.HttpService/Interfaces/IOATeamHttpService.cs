using Pms.HttpService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pms.HttpService.Interfaces
{
    /// <summary>
    /// Http服务：团队
    /// </summary>
    public interface IOATeamHttpService
    {
        /// <summary>
        /// 获取团队树列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="justValid">是否有效</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OATeam>> GetListAsync(int type = -1, bool justValid = true);

        /// <summary>
        /// 获取团队树列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="justValid">是否有效</param>
        /// <returns>人员列表</returns>
        Task<IEnumerable<OATeamTree>> GetListTreeAsync(int type = -1, bool justValid = true);

        /// <summary>
        /// 获取团队实体
        /// </summary>
        /// <returns>人员列表</returns>
        Task<OATeam> GetAsync(Guid id);
    }
}
