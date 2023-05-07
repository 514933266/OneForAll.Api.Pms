using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 项目任务
    /// </summary>
    public class PmsTaskDto
    {
        public PmsTaskDto()
        {
            UserIds = new HashSet<Guid>();
            Files = new HashSet<PmsTaskFileDto>();
        }

        public Guid Id { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 需求id
        /// </summary>
        public Guid RequirementId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建者人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public byte Priority { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PmsTaskStatusEnum Status { get; set; }

        /// <summary>
        /// 任务详情
        /// </summary>
        public virtual ICollection<Guid> UserIds { get; set; }

        /// <summary>
        /// 任务附件
        /// </summary>
        public virtual ICollection<PmsTaskFileDto> Files { get; set; }

    }
}
