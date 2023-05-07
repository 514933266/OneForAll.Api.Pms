using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 代码结构
    /// </summary>
    public class PmsCodeStructureDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public PmsCodeStructureTypeEnum Type { get; set; }

        /// <summary>
        /// 模板Json（当Type=File时有效）
        /// </summary>
        public string TemplateJson { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}