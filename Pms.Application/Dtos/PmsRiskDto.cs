using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 项目风险
    /// </summary>
    public class PmsRiskDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 里程碑时间
        /// </summary>
        public Guid MilestoneId { get; set; }

        /// <summary>
        /// 所属里程碑
        /// </summary>
        public string MilestoneTitle { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 状态 0未解决 1已解决 2已关闭
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// 严重等级 0 ~ 4
        /// </summary>
        public byte Priority { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
