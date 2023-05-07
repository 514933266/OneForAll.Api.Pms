using Pms.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Models
{
    /// <summary>
    /// 更新表实体字段
    /// </summary>
    public class PmsEntityTableUpdateFieldForm
    {
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        [Required]
        public IEnumerable<PmsEntityFieldForm> Fields { get; set; }
    }
}
