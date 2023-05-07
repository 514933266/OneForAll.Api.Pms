using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Public.Models
{
    /// <summary>
    /// 登录用户
    /// </summary>
    public class LoginUser
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 所属机构Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 对应OA档案id
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 机构是否默认
        /// </summary>
        public bool IsDefaultTenant { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否默认（默认用户不可删除）
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
