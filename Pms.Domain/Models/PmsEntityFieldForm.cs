using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 字段表单
    /// </summary>
    public class PmsEntityFieldForm
    {
        /// <summary>
        /// 字段名称
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
        /// 数据库类型
        /// </summary>
        [Required]
        [StringLength(50)]
        public string DbType { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        [Required]
        public bool IsNullable { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
