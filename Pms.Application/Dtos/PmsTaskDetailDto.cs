using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 任务明细
    /// </summary>
    public class PmsTaskDetailDto
    {
        /// <summary>
        /// 实体id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户岗位 0设计 1前端开发 2后台开发 3测试 4 需求
        /// </summary>
        public string UserJob { get; set; }

        /// <summary>
        /// 预估工时
        /// </summary>
        public double EstimateHours { get; set; }

        /// <summary>
        /// 实际工时
        /// </summary>
        public double ActualHours { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 状态 0未开始 1进行中 2暂停 3完成
        /// </summary>
        public PmsTaskStatusEnum Status { get; set; }

        /// <summary>
        /// 任务记录
        /// </summary>
        public virtual ICollection<PmsTaskRecordDto> Records { get; set; }
    }
}
