using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using OneForAll.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 子表：需求历史记录
    /// </summary>
    public class PmsRequirementRecord : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime
    {

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
        /// 版本更新备注
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string VersionRemark { get; set; }
    }
}
