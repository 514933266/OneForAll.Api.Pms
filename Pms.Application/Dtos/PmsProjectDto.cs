using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 开发项目
    /// </summary>
    public class PmsProjectDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目令牌（拥有该令牌者可以克隆项目信息）
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 项目图标
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 可见范围 0：私有 1:登录可见 2：公开
        /// </summary>
        public PmsProjectScopeEnum Scope { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否星标项目
        /// </summary>
        public bool IsStar { get; set; }
    }
}
