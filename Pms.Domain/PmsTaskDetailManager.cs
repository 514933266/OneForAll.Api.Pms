using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using OneForAll.Core.Upload;
using OneForAll.EFCore;
using OneForAll.File;
using OneForAll.File.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pms.Public.Models;
using Microsoft.AspNetCore.Http;
using OneForAll.Core.OAuth;
using Pms.Domain.Aggregates;
using AutoMapper.Execution;
using System.Runtime.InteropServices;

namespace Pms.Domain
{
    /// <summary>
    /// 任务记录
    /// </summary>
    public class PmsTaskDetailManager : PmsBaseManager, IPmsTaskDetailManager
    {
        private readonly IPmsTaskRepository _taskRepository;
        private readonly IPmsTaskRecordRepository _repository;
        private readonly IPmsTaskMemberContactRepository _memberContactRepository;

        public PmsTaskDetailManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsTaskRepository taskRepository,
            IPmsTaskRecordRepository repository,
            IPmsTaskMemberContactRepository memberContactRepository) : base(mapper, httpContextAccessor)
        {
            _repository = repository;
            _taskRepository = taskRepository;
            _memberContactRepository = memberContactRepository;
        }

        /// <summary>
        /// 获取任务指派明细
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">关键字</param>
        /// <returns></returns>
        public async Task<IEnumerable<PmsTaskDetailAggregate>> GetListAsync(Guid projectId, Guid id)
        {
            var result = new HashSet<PmsTaskDetailAggregate>();
            var task = await _taskRepository.FindAsync(id);
            if (task == null || task.PmsProjectId != projectId)
                return result;

            var members = await _memberContactRepository.GetListByTaskAsync(new Guid[] { id });
            var records = await _repository.GetListAsync(id);

            members.ForEach(member =>
            {
                var item = result.FirstOrDefault(w => w.Contact.SysUserId == member.Contact.SysUserId);
                var details = records.Where(w => w.SysUserId == member.Contact.SysUserId).ToList().OrderByDescending(o => o.CreateTime);
                if (item == null)
                {
                    item = new PmsTaskDetailAggregate()
                    {
                        Contact = member.Contact,
                        Member = member.Member
                    };
                    item.Records.AddRange(details);
                    result.Add(item);
                }
            });

            return result;
        }
    }
}
