using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 表实体
    /// </summary>
    public class PmsEntityTableForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Alias { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
