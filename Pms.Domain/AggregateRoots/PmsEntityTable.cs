using OneForAll.Core;
using OneForAll.Core.DDD;
using Pms.Domain.Enums;
using Pms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 表实体
    /// </summary>
    public class PmsEntityTable : AggregateRoot<Guid>, ICreator<Guid>, IUpdateTime
    {
        /// <summary>
        /// 所属项目id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Alias { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 字段Json
        /// </summary>
        [Required]
        public string FiledJson { get; set; } = "";

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
