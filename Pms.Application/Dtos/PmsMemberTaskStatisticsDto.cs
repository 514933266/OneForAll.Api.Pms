using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Application.Dtos
{
    /// <summary>
    /// 成员任务统计
    /// </summary>
    public class PmsMemberTaskStatisticsDto
    {
        /// <summary>
        /// 成员
        /// </summary>
        public PmsTeamMemberDto Member { get; set; }

        /// <summary>
        /// 总Bug数量
        /// </summary>
        public int TotalBugCount { get; set; }

        /// <summary>
        /// 未完成Bug数量
        /// </summary>
        public int NotFinishBugCount { get; set; }

        /// <summary>
        /// 总任务数量
        /// </summary>
        public int TotalTaskCount { get; set; }

        /// <summary>
        /// 未完成任务数量
        /// </summary>
        public int NotFinishTaskCount { get; set; }

        /// <summary>
        /// 总工时
        /// </summary>
        public double TotalHour { get; set; }
    }
}
