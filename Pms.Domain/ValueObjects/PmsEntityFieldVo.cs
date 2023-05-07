using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.ValueObjects
{
    /// <summary>
    /// 实体字段
    /// </summary>
    public class PmsEntityFieldVo
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
        /// 序号
        /// </summary>
        [Required]
        public int SortNumber { get; set; }

        /// <summary>
        /// 是否标识
        /// </summary>
        [Required]
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        [Required]
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; } = "";

        /// <summary>
        /// 默认值
        /// </summary>
        [Required]
        [StringLength(100)]
        public string DefaultValue { get; set; } = "";
    }
}
