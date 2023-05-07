using Pms.Domain.ValueObjects;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using OneForAll.Core.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 实体：团队成员
    /// </summary>
    public class PmsMember : AggregateRoot<Guid>, ICreateTime
    {
        /// <summary>
        /// 租户id
        /// </summary>
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 系统账号id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 系统账号
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; } = "";

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Job { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}