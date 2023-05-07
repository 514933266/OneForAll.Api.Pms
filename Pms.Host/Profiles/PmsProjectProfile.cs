using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Host.Profiles
{
    public class PmsProjectProfile : Profile
    {
        public PmsProjectProfile()
        {
            CreateMap<PmsProject, PmsProjectDto>();
            CreateMap<PmsProjectForm, PmsProject>();
        }
    }
}
