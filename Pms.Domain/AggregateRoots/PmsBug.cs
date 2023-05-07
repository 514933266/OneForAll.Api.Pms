using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using Pms.Domain.Enums;
using Pms.Domain.Models;
using Pms.Public.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// Bug表
    /// </summary>
    public class PmsBug : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime
    {
        /// <summary>
        /// 所属项目Id
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// 登录用户id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 类型 代码错误, 前端样式, 设计缺陷, 性能优化, 安全相关
        /// </summary>
        [Required]
        public PmsBugTypeEnum Type { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [Required]
        public byte Priority { get; set; }

        /// <summary>
        /// 严重程度 0轻微 1中等 2严重 3致命
        /// </summary>
        [Required]
        public PmsBugLevelEnum Level { get; set; }

        /// <summary>
        /// 状态 0未解决 1已解决 2已关闭
        /// </summary>
        [Required]
        public PmsBugStatusEnum Status { get; set; }

        /// <summary>
        /// 完成Bug
        /// </summary>
        public void Finish()
        {
            UpdateTime = DateTime.Now;
            Status = PmsBugStatusEnum.Finished;
        }

    }
}
