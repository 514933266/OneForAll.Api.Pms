using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class PmsDbConnectStringForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string ConnectString { get; set; }
    }
}
