using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pms.Host.Models
{
    /// <summary>
    /// 跨域配置模型
    /// </summary>
    public class CorsConfig
    {
        public string[] Origins { get; set; }
    }
}
