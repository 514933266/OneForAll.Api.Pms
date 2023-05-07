using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Models;

namespace Pms.Host.Profiles
{
    public class PmsCodeDenerationRecordProfile : Profile
    {
        public PmsCodeDenerationRecordProfile()
        {
            CreateMap<PmsCodeDenerationRecord, PmsCodeDenerationRecordDto>();
        }
    }
}
