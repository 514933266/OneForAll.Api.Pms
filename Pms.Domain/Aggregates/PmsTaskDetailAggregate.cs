using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Aggregates
{
    /// <summary>
    /// 任务明细
    /// </summary>
    public class PmsTaskDetailAggregate
    {
        public PmsTaskDetailAggregate()
        {
            Records = new List<PmsTaskRecord>();
        }

        /// <summary>
        /// 对应的项目成员
        /// </summary>
        public PmsMember Member { get; set; }

        /// <summary>
        /// 成员关系
        /// </summary>
        public PmsTaskMemberContact Contact { get; set; }

        /// <summary>
        /// 任务记录
        /// </summary>
        public List<PmsTaskRecord> Records { get; set; }
    }
}
