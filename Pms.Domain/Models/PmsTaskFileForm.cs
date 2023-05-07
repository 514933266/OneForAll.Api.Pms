using OneForAll.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 任务附件
    /// </summary>
    public class PmsTaskFileForm : Entity<Guid>
    {

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [StringLength(50)]
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
        [StringLength(300)]
        public string Url { get; set; }
    }
}
