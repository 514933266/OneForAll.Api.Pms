using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Enums
{
    /// <summary>
    /// 代码结构类型
    /// </summary>
    public enum PmsCodeStructureTypeEnum
    {
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder = 1000,

        /// <summary>
        /// 文件夹
        /// </summary>
        EntityFolder = 1001,

        /// <summary>
        /// 代码文件
        /// </summary>
        File = 2000,

        /// <summary>
        /// 实体文件
        /// </summary>
        EntityFile = 2001
    }
}
