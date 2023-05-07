using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Enums
{
    /// <summary>
    /// Bug等级
    /// </summary>
    public enum PmsBugLevelEnum
    {
        None = -1,

        /// <summary>
        /// 轻微
        /// </summary>
        Slight = 0,

        /// <summary>
        /// 中等
        /// </summary>
        Medium = 1,

        /// <summary>
        /// 严重
        /// </summary>
        Serious = 2,

        /// <summary>
        /// 致命
        /// </summary>
        Deadly = 3
    }
}
