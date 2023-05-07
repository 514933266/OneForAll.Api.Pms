using Pms.Domain.ValueObjects;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class PmsMemberForm : Entity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 岗位职级
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Job { get; set; }

    }
}