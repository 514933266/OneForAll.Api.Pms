using Pms.Domain.AggregateRoots;
using Pms.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Domain.Aggregates
{
    /// <summary>
    /// 聚合：项目成员任务统计
    /// </summary>
    public class PmsMemberTaskStatistics
    {
        public PmsMemberTaskStatistics(PmsMember member, ICollection<PmsTask> tasks, ICollection<PmsBug> bugs)
        {
            Member = member;
            Tasks = tasks;
            Bugs = bugs;
            Initialize();
        }

        /// <summary>
        /// 成员
        /// </summary>
        public PmsMember Member { get; set; }

        /// <summary>
        /// 成员任务
        /// </summary>
        private ICollection<PmsTask> Tasks { get; set; }

        /// <summary>
        /// 成员Bug
        /// </summary>
        private ICollection<PmsBug> Bugs { get; set; }

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

        // 计算数据
        private void Initialize()
        {
            // 计算Bugs
            TotalBugCount = Bugs.Count();
            NotFinishBugCount = Bugs.Count(w => w.Status == 0);

            // 计算任务
            foreach (var task in Tasks)
            {
                //var taskUser = task.PmsTaskMemberContacts.FirstOrDefault(w => w.SysUserId == Member.SysUserId);
                //if (taskUser != null)
                //{
                //    TotalTaskCount++;
                //    TotalHour += taskUser.ActualHours;
                //    if (taskUser.Status < PmsTaskStatusEnum.Finish) NotFinishTaskCount++;
                //}
            }
        }
    }
}
