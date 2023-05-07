using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace Pms.Host.Profiles
{
    public class PmsRequirementRecordProfile : Profile
    {
        public PmsRequirementRecordProfile()
        {
            CreateMap<PmsRequirementRecord, PmsRequirementRecordDto>();
        }
    }
}

