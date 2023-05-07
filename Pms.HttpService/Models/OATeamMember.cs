using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.HttpService.Models
{
    /// <summary>
    /// 实体：团队成员
    /// </summary>
    public class OATeamMember
    {
        /// <summary>
        /// 对应TeamPersonContact.Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 人员id
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 是否管理者
        /// </summary>
        public bool IsLeader { get; set; }

        /// <summary>
        /// 所属组织id
        /// </summary>
        public Guid TeamId { get; set; }

        /// <summary>
        /// 所属组织名称
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 数据更新时间(用于离职或调动)
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否有效（离职或者调动为false）
        /// </summary>
        public bool IsValid { get; set; }
    }
}
