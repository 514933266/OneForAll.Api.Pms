using OneForAll.Core;
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
    /// 代码生成日志
    /// </summary>
    public class PmsCodeDenerationRecord : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
