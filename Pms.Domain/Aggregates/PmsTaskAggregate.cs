using NPOI.OpenXml4Net.OPC.Internal;
using OneForAll.Core.Extension;
using Pms.Domain.AggregateRoots;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pms.Domain.Aggregates
{
    /// <summary>
    /// 项目任务
    /// </summary>
    public class PmsTaskAggregate
    {
        public PmsTaskAggregate()
        {
            MemberContacts = new HashSet<PmsTaskMemberContactAggregate>();
        }

        /// <summary>
        /// 任务
        /// </summary>
        public PmsTask Task { get; set; }

        /// <summary>
        /// 成员关系
        /// </summary>
        public ICollection<PmsTaskMemberContactAggregate> MemberContacts { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public ICollection<PmsTaskFile> Files { get; set; }

        /// <summary>
        /// 分配任务给成员
        /// </summary>
        public void AssignSubTasks(IEnumerable<PmsMember> members)
        {
            members.ForEach(e =>
            {
                var subTask = new PmsTaskMemberContact()
                {
                    PmsTaskId = Task.Id,
                    SysUserId = e.SysUserId
                };
                if (!MemberContacts.Any(w => w.Member.SysUserId == e.SysUserId))
                {
                    MemberContacts.Add(new PmsTaskMemberContactAggregate()
                    {
                        Member = e,
                        Contact = subTask
                    });
                }
            });
        }

        /// <summary>
        /// 分配任务给成员
        /// </summary>
        public void AddFiles(IEnumerable<PmsTaskFile> files)
        {
            files.ForEach(e =>
            {
                e.PmsTaskId = Task.Id;
            });
            Files = files.ToList();
        }

        /// <summary>
        /// 删除文件资源
        /// </summary>
        /// <param name="files"></param>
        public void DeleteFileResource(IEnumerable<PmsTaskFile> files)
        {
            files.ForEach(e =>
            {
                if (File.Exists(e.FileName))
                {
                    File.Delete(e.FileName);
                }
            });
        }
    }
}
