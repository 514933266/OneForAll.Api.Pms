using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 关联表信息
    /// </summary>
    public class PmsEntityTableContact : AggregateRoot<Guid>
    {
        /// <summary>
        /// 项目id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 来源表id
        /// </summary>
        [Required]
        public Guid SourceTableId { get; set; }


        /// <summary>
        /// 目标表id
        /// </summary>
        [Required]
        public Guid TargetTableId { get; set; }

    }
}
