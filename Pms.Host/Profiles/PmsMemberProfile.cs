using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Host.Profiles
{
    public class PmsMemberProfile : Profile
    {
        public PmsMemberProfile()
        {
            CreateMap<PmsMember, PmsTeamMemberDto>();
            CreateMap<PmsMember, PmsProjectMemberDto>()
               .ForMember(t => t.UserId, a => a.MapFrom(e => e.SysUserId));
            CreateMap<PmsMemberForm, PmsMember>();
            CreateMap<PmsMemberBindAccountForm, PmsMember>()
               .ForMember(t => t.SysUserId, a => a.MapFrom(e => e.UserId));
        }
    }
}
