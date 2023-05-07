using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using OneForAll.Core;
using OneForAll.Core.ORM;
using OneForAll.Core.DDD;
using Pms.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 子表：项目需求
    /// </summary>
    public class PmsRequirement : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime, IUpdateTime
    {
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
        /// 所属项目Id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName ="datetime")]
        public DateTime CreateTime { get; set; }

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
        /// 备注
        /// </summary>
        [MaxLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
    }
}