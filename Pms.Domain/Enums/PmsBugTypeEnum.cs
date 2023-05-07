using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Enums
{
    /// <summary>
    /// Bug类型
    /// </summary>
    public enum PmsBugTypeEnum
    {
        None = -1,

        /// <summary>
        /// 代码错误
        /// </summary>
        CodeError = 0,

        /// <summary>
        /// 前端样式
        /// </summary>
        StyleError = 1,

        /// <summary>
        /// 设计缺陷
        /// </summary>
        DesignError = 2,

        /// <summary>
        /// 性能优化
        /// </summary>
        Optimization = 3,

        /// <summary>
        /// 安全相关
        /// </summary>
        Security = 4
    }
}
