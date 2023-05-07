using OneForAll.Core;
using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Aggregates
{
    /// <summary>
    /// 代码结构树
    /// </summary>
    public class PmsCodeStructureTreeAggr : PmsCodeStructure, IChildren<PmsCodeStructureTreeAggr>
    {
        /// <summary>
        /// 子级
        /// </summary>
        public IEnumerable<PmsCodeStructureTreeAggr> Children { get; set; }
    }
}
