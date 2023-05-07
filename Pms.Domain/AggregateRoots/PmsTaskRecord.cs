using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 任务记录
    /// </summary>
    public class PmsTaskRecord : AggregateRoot<Guid>, ICreateTime
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        [Required]
        public Guid PmsTaskId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 状态 0未开始 1进行中 2暂停 3完成
        /// </summary>
        [Required]
        public PmsTaskStatusEnum Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName ="datetime")]
        public DateTime CreateTime { get; set; }
    }
}
