using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 团队成员（绑定系统账号）
    /// </summary>
    public class PmsMemberBindAccountForm
    {
        /// <summary>
        /// 系统用户id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
    }
}
