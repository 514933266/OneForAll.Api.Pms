using AutoMapper;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Interfaces;
using Pms.Domain.Models;
using Pms.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Pms.Domain.Enums;
using AutoMapper.Execution;

namespace Pms.Domain
{
    /// <summary>
    /// 项目
    /// </summary>
    public class PmsProjectManager : PmsBaseManager, IPmsProjectManager
    {
        private readonly IPmsProjectRepository _reposiotry;
        private readonly IPmsMemberRepository _memberRepository;
        private readonly IPmsProjectMemberContactRepository _contactReposiotry;

        public PmsProjectManager(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPmsProjectRepository reposiotry,
            IPmsMemberRepository memberRepository,
            IPmsProjectMemberContactRepository contactReposiotry) : base(mapper, httpContextAccessor)
        {
            _reposiotry = reposiotry;
            _memberRepository = memberRepository;
            _contactReposiotry = contactReposiotry;
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="scope">项目范围</param>
        /// <param name="name">项目名称</param>
        /// <returns>项目列表</returns>
        public async Task<IEnumerable<PmsProject>> GetListAsync(PmsProjectVisitEnum scope, string name)
        {
            var data = await _reposiotry.GetListPersonalAsync(LoginUser.Id, name);
            switch (scope)
            {
                case PmsProjectVisitEnum.Team:
                    return data.Where(w => w.CreatorId != LoginUser.Id).ToList();
                case PmsProjectVisitEnum.Self:
                    return data.Where(w => w.CreatorId == LoginUser.Id).ToList();
                case PmsProjectVisitEnum.Star:
                    return data.Where(w => w.IsStar).ToList();
                default:
                    return data;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(PmsProjectForm form)
        {
            var data = _mapper.Map<PmsProjectForm, PmsProject>(form);
            data.SysTenantId = LoginUser.TenantId;
            data.CreatorId = LoginUser.Id;
            data.CreatorName = LoginUser.Name;

            return await ResultAsync(() => _reposiotry.AddAsync(data));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(PmsProjectForm form)
        {
            var data = await _reposiotry.FindAsync(form.Id);
            if (data == null)
                return BaseErrType.DataNotFound;

            if (data.CreatorId != LoginUser.Id)
                return BaseErrType.NotAllow;

            _mapper.Map(form, data);
            return await ResultAsync(() => _reposiotry.SaveChangesAsync());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(Guid id)
        {
            var project = await _reposiotry.FindAsync(id);
            if (project == null)
                return BaseErrType.DataNotFound;
            if (project.CreatorId != LoginUser.Id)
                return BaseErrType.NotAllow;

            return await ResultAsync(() => _reposiotry.DeleteAsync(project));
        }

        /// <summary>
        /// 设为星标项目
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SetToStarAsync(Guid id)
        {
            var project = await _reposiotry.FindAsync(id);
            if (project == null)
                return BaseErrType.DataNotFound;
            if (project.CreatorId != LoginUser.Id)
                return BaseErrType.NotAllow;

            project.IsStar = !project.IsStar;
            return await ResultAsync(() => _reposiotry.SaveChangesAsync());
        }

        /// <summary>
        /// 检查项目授权
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CheckProjectAuthorization(Guid id)
        {
            var pass = false;
            var data = await _reposiotry.FindAsync(id);
            if (data == null)
                return false;
            if (data.CreatorId == LoginUser.Id)
                return true;

            switch (data.Scope)
            {
                case PmsProjectScopeEnum.Internal:
                    {
                        var members = await _contactReposiotry.GetListByProjectAsync(id);
                        if (members.Any(w => w.SysUserId == LoginUser.Id))
                            pass = true;
                    };
                    break;
                case PmsProjectScopeEnum.Public:
                    {
                        var members = await _memberRepository.GetListAsync();
                        if (members.Any(w => w.SysUserId == LoginUser.Id))
                            pass = true;
                    }
                    break;
            }
            return pass;
        }
    }
}
