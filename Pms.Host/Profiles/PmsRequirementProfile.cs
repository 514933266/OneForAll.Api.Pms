using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Host.Profiles
{
    public class PmsRequirementProfile : Profile
    {
        public PmsRequirementProfile()
        {
            CreateMap<PmsRequirement, PmsRequirementRecord>()
                .ForMember(t => t.PmsRequirementId, a => a.MapFrom(e => e.Id));
            CreateMap<PmsRequirement, PmsRequirementDto>()
                .ForMember(t => t.ProjectId, a => a.MapFrom(e => e.PmsProjectId));
            CreateMap<PmsRequirementForm, PmsRequirement>();
        }
    }
}
