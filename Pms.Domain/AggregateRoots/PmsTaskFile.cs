using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 任务文件
    /// </summary>
    public class PmsTaskFile : AggregateRoot<Guid>
    {
        /// <summary>
        /// 任务id
        /// </summary>
        [Required]
        public Guid PmsTaskId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FileName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [Required]
        public int FileSize { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [Required]
        [MaxLength(300)]
        public string Url { get; set; }
    }
}
