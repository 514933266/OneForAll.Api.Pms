using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Enums
{
    /// <summary>
    /// 项目查看
    /// </summary>
    public enum PmsProjectVisitEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,

        /// <summary>
        /// 团队项目
        /// </summary>
        Team = 1,

        /// <summary>
        /// 个人项目
        /// </summary>
        Self = 2,

        /// <summary>
        /// 星标项目
        /// </summary>
        Star = 3
    }
}
