using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using OneForAll.Core;
using OneForAll.Core.ORM;
using OneForAll.Core.DDD;
using Pms.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// �ӱ���Ŀ����
    /// </summary>
    public class PmsRequirement : AggregateRoot<Guid>, ICreator<Guid>, ICreateTime, IUpdateTime
    {
        /// <summary>
        /// ����
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// ������ĿId
        /// </summary>
        [Required]
        public Guid PmsProjectId { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        [Required]
        [Column(TypeName ="datetime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ������Id
        /// </summary>
        [Required]
        public Guid CreatorId { get; set; }
        
        /// <summary>
        /// ����������
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string CreatorName { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        [MaxLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdateTime { get; set; }
    }
}