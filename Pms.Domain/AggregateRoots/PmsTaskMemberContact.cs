using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.ORM;
using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Pms.Domain.AggregateRoots
{
    /// <summary>
    /// 任务用户
    /// </summary>
    public class PmsTaskMemberContact : AggregateRoot<Guid>
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        [Required]
        public Guid PmsTaskId { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Required]
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 预估工时
        /// </summary>
        [Required]
        public double EstimateHours { get; set; }

        /// <summary>
        /// 实际工时
        /// </summary>
        [Required]
        public double ActualHours { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Column(TypeName ="datetime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 状态 0未开始 1进行中 2暂停 3完成
        /// </summary>
        [Required]
        public PmsTaskStatusEnum Status { get; set; }

        /// <summary>
        /// 根据状态修改时间
        /// </summary>
        public void ChangeTime()
        {
            switch (Status)
            {
                case PmsTaskStatusEnum.Start: if (StartTime == null) StartTime = DateTime.Now; break;
                case PmsTaskStatusEnum.Finish: FinishTime = DateTime.Now; break;
            }
        }

        /// <summary>
        /// 根据任务记录计算总工时
        /// </summary>
        public void CalculateActualHours(IEnumerable<PmsTaskRecord> records)
        {
            var totalHours = 0d;
            var sortList = records
                .Where(w => w.SysUserId.Equals(SysUserId))
                .OrderBy(e => e.CreateTime)
                .ToList();

            PmsTaskRecord prev = null;
            sortList.ForEach(e =>
            {
                if (prev != null)
                {
                    if (prev.Status == PmsTaskStatusEnum.Start && (e.Status == PmsTaskStatusEnum.Stop || e.Status == PmsTaskStatusEnum.Finish))
                    {
                        totalHours += (e.CreateTime - prev.CreateTime).TotalHours;
                    }
                }
                prev = e;
            });
            ActualHours = totalHours;
        }
    }
}
