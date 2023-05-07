﻿using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.HttpService.Models
{
    /// <summary>
    /// 实体：组织架构树
    /// </summary>
    public class OATeamTree: IParent<Guid>, IEntity<Guid>, IChildren<OATeamTree>
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主管id
        /// </summary>
        public Guid LeaderId { get; set; }

        /// <summary>
        /// 主管名称
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 子级
        /// </summary>

        public IEnumerable<OATeamTree> Children { get; set; }

    }
}
