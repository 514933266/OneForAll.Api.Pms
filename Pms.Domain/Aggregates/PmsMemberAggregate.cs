using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Aggregates
{
    /// <summary>
    /// 项目
    /// </summary>
    public class PmsMemberAggregate : PmsProject
    {
        /// <summary>
        /// 成员
        /// </summary>
        public ICollection<PmsMember> Members { get; set; }
    }
}
