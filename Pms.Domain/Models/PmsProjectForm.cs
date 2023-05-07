using OneForAll.Core.DDD;
using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 项目
    /// </summary>
    public class PmsProjectForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 可见范围 0：私有 1:登录可见 2：公开
        /// </summary>
        [Required]
        public PmsProjectScopeEnum Scope { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(300)]
        public string Remark { get; set; }

    }
}
