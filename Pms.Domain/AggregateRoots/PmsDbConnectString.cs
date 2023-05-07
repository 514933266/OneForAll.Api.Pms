using OneForAll.Core.DDD;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class PmsDbConnectString : AggregateRoot<Guid>, ICreator<Guid>, IUpdateTime
    {
        /// <summary>
        /// 项目id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string ConnectString { get; set; }

        /// <summary>
        /// 创建人
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
        /// 最后修改时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
    }
}
