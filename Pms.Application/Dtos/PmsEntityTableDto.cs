using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pms.Domain.ValueObjects;
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
    /// 表实体
    /// </summary>
    public class PmsEntityTableDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

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
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public IEnumerable<PmsEntityFieldVo> Fields { get; set; } = new List<PmsEntityFieldVo>();

        /// <summary>
        /// 表关联
        /// </summary>
        public IEnumerable<PmsEntityTableDto> Contacts { get; set; } = new List<PmsEntityTableDto>();
    }
}
