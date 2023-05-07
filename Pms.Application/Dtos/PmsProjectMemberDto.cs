using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 项目成员
    /// </summary>
    public class PmsProjectMemberDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Job { get; set; }
    }
}
