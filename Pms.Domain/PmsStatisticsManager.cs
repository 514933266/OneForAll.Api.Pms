using Pms.Domain.Aggregates;
using Pms.Domain.Interfaces;
using Pms.Domain.Repositorys;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain
{
    /// <summary>
    /// 项目统计
    /// </summary>
    public class PmsStatisticsManager : IPmsStatisticsManager
    {
        private readonly IPmsProjectRepository _projectReposiotry;
        public PmsStatisticsManager(IPmsProjectRepository projectReposiotry)
        {
            _projectReposiotry = projectReposiotry;
        }

        /// <summary>
        /// 获取项目成员任务统计
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>统计列表</returns>
        public async Task<IEnumerable<PmsMemberTaskStatistics>> GetMemberTasksAsync(Guid projectId)
        {
            var statistics = new List<PmsMemberTaskStatistics>();
            var project = await _projectReposiotry.GetWithDetailsAsync(projectId);
            if (project != null)
            {
                //var users = project.PmsProjectMemberContacts.Select(s => s.PmsMember);
                //users.ForEach(e =>
                //{
                //    var bugs = project.PmsBugs
                //        .Where(w => w.SysUserId == e.SysUserId)
                //        .ToList();

                //    var tasks = project.PmsTasks
                //        .Where(w => w.PmsTaskMemberContacts.Any(w1 => w1.SysUserId == e.SysUserId))
                //        .ToList();

                //    var member = new PmsMemberTaskStatistics(e, tasks, bugs);
                //    statistics.Add(member);
                //});
            }
            return statistics;
        }
    }
}
