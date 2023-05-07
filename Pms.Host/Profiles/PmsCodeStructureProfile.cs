using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Models;

namespace Pms.Host.Profiles
{
    public class PmsCodeStructureProfile : Profile
    {
        public PmsCodeStructureProfile()
        {
            CreateMap<PmsCodeStructure, PmsCodeStructureDto>();
            CreateMap<PmsCodeStructure, PmsCodeStructureTreeDto>();
            CreateMap<PmsCodeStructure, PmsCodeStructureTreeAggr>();
            CreateMap<PmsCodeStructureForm, PmsCodeStructure>();
        }
    }
}

