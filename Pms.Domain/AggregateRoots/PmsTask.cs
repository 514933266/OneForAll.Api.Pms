using Pms.Domain.Models;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using OneForAll.Core.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Pms.Domain.Enums;
using Pms.Public.Models;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 任务指派表
    /// </summary>
    public class PmsTask : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime
    {
        /// <summary>
        /// 所属项目Id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 需求Id
        /// </summary>
        [Required]
        public Guid PmsRequirementId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Required]
        public byte Priority { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public PmsTaskStatusEnum Status { get; set; }

    }
}