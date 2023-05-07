using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 项目任务
    /// </summary>
    public class PmsTaskForm : Entity<Guid>
    {
        /// <summary>
        /// 需求id
        /// </summary>
        [Required]
        public Guid RequirementId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Required]
        [Range(0, 4)]
        public byte Priority { get; set; }

        /// <summary>
        /// 指派人
        /// </summary>
        [Required]
        public ICollection<Guid> UserIds { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public ICollection<PmsTaskFileForm> Files { get; set; }
    }
}
