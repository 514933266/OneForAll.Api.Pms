using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Enums
{
    /// <summary>
    /// 枚举：任务状态
    /// </summary>
    public enum PmsTaskStatusEnum
    {
        /// <summary>
        /// 无
        /// </summary>
        None = -1,

        /// <summary>
        /// 未开始
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 开始
        /// </summary>
        Start,

        /// <summary>
        /// 暂停
        /// </summary>
        Stop,

        /// <summary>
        /// 结束
        /// </summary>
        Finish
    }
}
