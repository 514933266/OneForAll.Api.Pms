using AutoMapper;
using OneForAll.Core.Extension;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using Pms.Domain.ValueObjects;

namespace Pms.Host.Profiles
{
    public class PmsEntityTableProfile : Profile
    {
        public PmsEntityTableProfile()
        {
            CreateMap<PmsEntityTable, PmsEntityTableDto>();
            CreateMap<PmsEntityTableForm, PmsEntityTable>();
            CreateMap<PmsEntityTableUpdateFieldForm, PmsEntityTable>()
                .ForMember(t => t.FiledJson, a => a.MapFrom(e => e.Fields.ToJson()));
            CreateMap<PmsEntityFieldForm, PmsEntityFieldVo>();
        }
    }
}
