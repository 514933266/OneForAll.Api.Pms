using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 需求历史
    /// </summary>
    public class PmsRequirementRecordDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 版本更新备注
        /// </summary>
        public string VersionRemark { get; set; }
    }
}
