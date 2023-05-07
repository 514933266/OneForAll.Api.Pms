using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pms.Domain.Models;
using Pms.Public.Models;
using Microsoft.AspNetCore.Http;

namespace Pms.Domain
{
    /// <summary>
    /// 项目成员
    /// </summary>
    public class PmsMemberManager : PmsBaseManager, IPmsMemberManager
    {
        private readonly IPmsMemberRepository _memberRepository;
        private readonly IPmsProjectMemberContactRepository _memberContactRepository;
        public PmsMemberManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsMemberRepository memberRepository,
            IPmsProjectMemberContactRepository memberContactRepository) : base(mapper, httpContextAccessor)
        {
            _memberRepository = memberRepository;
            _memberContactRepository = memberContactRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <returns>成员列表</returns>
        public async Task<IEnumerable<PmsMember>> GetListAsync(Guid projectId)
        {
            return await _memberContactRepository.GetListByProjectAsync(projectId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="userIds">用户id集合</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(Guid projectId, IEnumerable<Guid> userIds)
        {
            var data = new List<PmsProjectMemberContact>();
            var existsDatas = await _memberContactRepository.GetListByProjectAsync(projectId);

            var users = await _memberRepository.GetListAsync(userIds);
            users.ForEach(e =>
            {
                if (!existsDatas.Any(w => w.SysUserId == e.SysUserId))
                {
                    data.Add(new PmsProjectMemberContact()
                    {
                        PmsProjectId = projectId,
                        PmsMemberId = e.Id
                    });
                }
            });
            if (data.Any())
            {
                return await ResultAsync(() => _memberContactRepository.AddRangeAsync(data));
            }
            else
            {
                return BaseErrType.DataEmpty;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid projectId, Guid id)
        {
            var item = await _memberContactRepository.FindAsync(id);
            if (item != null)
            {
                if (item.PmsProjectId != projectId)
                    return BaseErrType.DataNotMatch;

                return await ResultAsync(() => _memberContactRepository.DeleteAsync(item));
            }
            return BaseErrType.DataNotFound;
        }
    }
}
