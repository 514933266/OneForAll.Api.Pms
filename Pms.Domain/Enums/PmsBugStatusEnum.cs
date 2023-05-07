using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Enums
{
    /// <summary>
    /// Bug状态
    /// </summary>
    public enum PmsBugStatusEnum
    {
        /// <summary>
        /// 未解决
        /// </summary>
        UnFinished = 0,

        /// <summary>
        /// 已解决
        /// </summary>
        Finished = 1,

        /// <summary>
        /// 已关闭
        /// </summary>
        Closed = 2
    }
}
