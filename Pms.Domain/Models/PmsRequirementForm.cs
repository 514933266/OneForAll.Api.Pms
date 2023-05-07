using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 需求
    /// </summary>
    public class PmsRequirementForm : Entity<Guid>
    {

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
        /// 备注
        /// </summary>
        [StringLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// 上一版本更新说明
        /// </summary>
        [StringLength(100)]
        public string VersionRemark { get; set; }
    }
}
