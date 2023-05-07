using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 团队成员
    /// </summary>
    public class PmsTeamMemberDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        public string Job { get; set; }
    }
}
