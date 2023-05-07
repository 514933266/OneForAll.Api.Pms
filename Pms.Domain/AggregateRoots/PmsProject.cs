using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using Pms.Domain.Enums;
using Pms.Domain.Models;
using Pms.Public.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 项目表
    /// </summary>
    public class PmsProject : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime
    {
        /// <summary>
        /// 所属机构id
        /// </summary>
        [Required]
        public Guid SysTenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 项目令牌（拥有该令牌者可以克隆项目信息）
        /// </summary>
        [MaxLength(32)]
        [Column(TypeName = "varchar(32)")]
        public string Token { get; set; }

        /// <summary>
        /// 项目图标
        /// </summary>
        [MaxLength(500)]
        public string IconUrl { get; set; }

        /// <summary>
        /// 可见范围 0：私有 1:登录可见 2：公开
        /// </summary>
        [Required]
        public PmsProjectScopeEnum Scope { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(300)]
        public string Remark { get; set; }

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
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否星标项目
        /// </summary>
        [Required]
        public bool IsStar { get; set; }

    }
}