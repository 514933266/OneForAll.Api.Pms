using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 风险
    /// </summary>
    public class PmsRiskForm : Entity<Guid>
    {
        /// <summary>
        /// 里程碑id
        /// </summary>
        [Required]
        public Guid MilestoneId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 状态 0未解决 1已解决 2已关闭
        /// </summary>
        [Required]
        public byte Status { get; set; }

        /// <summary>
        /// 严重等级 0 ~ 4
        /// </summary>
        [Required]
        [Range(0, 4)]
        public byte Priority { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(300)]
        public string Remark { get; set; }
    }
}
