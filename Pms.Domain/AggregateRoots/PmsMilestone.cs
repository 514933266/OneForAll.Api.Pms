using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 里程碑
    /// </summary>
    public class PmsMilestone : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime
    {
        /// <summary>
        /// 所属项目Id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 状态 0未实现 1已实现 2已关闭
        /// </summary>
        [Required]
        public byte Status { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName ="datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        [Required]
        [Column(TypeName ="datetime")]
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
