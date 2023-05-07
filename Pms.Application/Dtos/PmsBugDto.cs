using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 项目Bug
    /// </summary>
    public class PmsBugDto
    {
        public Guid Id { get; set; }

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
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 指派人id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 岗位职级
        /// </summary>
        public string UserJob { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 类型 代码错误, 前端样式, 设计缺陷, 性能优化, 安全相关
        /// </summary>
        public PmsBugTypeEnum Type { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public byte Priority { get; set; }

        /// <summary>
        /// 严重程度 0轻微 1中等 2严重 3致命
        /// </summary>
        public PmsBugLevelEnum Level { get; set; }

        /// <summary>
        /// 状态 0未解决 1已解决 2已关闭
        /// </summary>
        public PmsBugStatusEnum Status { get; set; }

    }
}
