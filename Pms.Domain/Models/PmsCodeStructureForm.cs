using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 代码结构
    /// </summary>
    public class PmsCodeStructureForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        [Required]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public PmsCodeStructureTypeEnum Type { get; set; }

        /// <summary>
        /// 模板Json（当Type=File时有效）
        /// </summary>
        public string TemplateJson { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Remark { get; set; }
    }
}
