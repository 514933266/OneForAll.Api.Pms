using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Aggregates
{
    /// <summary>
    /// 任务成员关系
    /// </summary>
    public class PmsTaskMemberContactAggregate
    {
        /// <summary>
        /// 成员关系
        /// </summary>
        public PmsTaskMemberContact Contact { get; set; }

        /// <summary>
        /// 对应的项目成员
        /// </summary>
        public PmsMember Member { get; set; }

    }
}
