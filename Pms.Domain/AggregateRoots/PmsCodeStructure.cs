using OneForAll.Core;
using OneForAll.Core.DDD;
using Pms.Domain.Enums;
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
    /// 代码结构
    /// </summary>
    public class PmsCodeStructure : AggregateRoot<Guid>, IParent<Guid>, ICreator<Guid>, IUpdateTime
    {
        /// <summary>
        /// 所属项目id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        [Required]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        public PmsCodeStructureTypeEnum Type { get; set; }

        /// <summary>
        /// 模板Json（当Type=File时有效）
        /// </summary>
        [Required]
        public string TemplateJson { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Remark { get; set; }

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
        public DateTime? UpdateTime { get; set; } = DateTime.Now;
    }
}
