using OneForAll.Core.DDD;
using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// Bug
    /// </summary>
    public class PmsBugForm : Entity<Guid>
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// 类型 0代码错误, 1前端样式, 2设计缺陷, 3性能优化, 4安全相关
        /// </summary>
        [Required]
        public PmsBugTypeEnum Type { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Required]
        [Range(0, 4)]
        public byte Priority { get; set; }

        /// <summary>
        /// 严重程度 0轻微 1中等 2严重 3致命
        /// </summary>
        [Required]
        [Range(0, 3)]
        public PmsBugLevelEnum Level { get; set; }

        /// <summary>
        /// 状态 0未解决 1已解决 2已关闭
        /// </summary>
        [Required]
        [Range(0, 2)]
        public PmsBugStatusEnum Status { get; set; }
    }
}
