﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pms.HttpService.Models
{
    /// <summary>
    /// 租户
    /// </summary>
    public class SysTenantForm
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 机构代码
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Code { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [StringLength(50)]
        public string Manager { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 企业地址
        /// </summary>
        [StringLength(300)]
        public string Address { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(300)]
        public string Description { get; set; }
    }
}
