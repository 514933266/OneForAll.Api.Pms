using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public class PmsDbConnectStringDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectString { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
