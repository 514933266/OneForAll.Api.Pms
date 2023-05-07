using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Enums
{
    /// <summary>
    /// 枚举：项目访问范围
    /// </summary>
    public enum PmsProjectScopeEnum
    {
        /// <summary>
        /// 私有
        /// </summary>
        Private,

        /// <summary>
        /// 团队的
        /// </summary>
        Internal,

        /// <summary>
        /// 公开
        /// </summary>
        Public
    }
}
