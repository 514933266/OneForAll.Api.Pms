using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Aggregates
{
    /// <summary>
    /// 项目bug
    /// </summary>
    public class PmsBugAggregate
    {
        /// <summary>
        /// Bug
        /// </summary>
        public PmsBug Bug { get; set; }

        /// <summary>
        /// 所属成员
        /// </summary>
        public PmsMember Member { get; set; }
    }
}
