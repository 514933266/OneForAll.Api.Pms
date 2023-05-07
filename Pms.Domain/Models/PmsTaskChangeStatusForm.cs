using OneForAll.Core.DDD;
using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 修改子任务状态
    /// </summary>
    public class PmsTaskChangeStatusForm
    {

        /// <summary>
        /// 任务id
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 状态 0未开始 1进行中 2暂停 3完成
        /// </summary>
        [Required]
        public PmsTaskStatusEnum Status { get; set; }

    }
}
