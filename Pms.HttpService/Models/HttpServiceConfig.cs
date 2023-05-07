using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pms.HttpService.Models
{
    /// <summary>
    /// 数据资源服务配置
    /// </summary>
    public class HttpServiceConfig
    {
        /// <summary>
        /// 权限验证接口
        /// </summary>
        public string SysPermissionCheck { get; set; } = "SysPermissionCheck";

        /// <summary>
        /// 租户
        /// </summary>
        public string SysTenant { get; set; } = "SysTenant";

        /// <summary>
        /// OA人员档案
        /// </summary>
        public string OAPerson { get; set; } = "OAPerson";

        /// <summary>
        /// OA团队
        /// </summary>
        public string OATeam { get; set; } = "OATeam";

        /// <summary>
        /// OA团队
        /// </summary>
        public string OATeamMember { get; set; } = "OATeamMember";

        /// <summary>
        /// Api日志
        /// </summary>
        public string SysApiLog { get; set; } = "SysApiLog";

        /// <summary>
        /// 消息通知
        /// </summary>
        public string UmsMessage { get; set; } = "UmsMessage";
    }
}
