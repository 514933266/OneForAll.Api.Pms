using OneForAll.Core;
using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 关联表：项目—开发人员
    /// </summary>
    public class PmsProjectMemberContact : AggregateRoot<Guid>
    {

        /// <summary>
        /// 项目Id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public Guid PmsMemberId { get; set; }
    }
}
