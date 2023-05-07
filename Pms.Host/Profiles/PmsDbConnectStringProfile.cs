using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;

namespace Pms.Host.Profiles
{

    public class PmsDbConnectStringProfile : Profile
    {
        public PmsDbConnectStringProfile()
        {
            CreateMap<PmsDbConnectString, PmsDbConnectStringDto>();
            CreateMap<PmsDbConnectStringForm, PmsDbConnectString>();
        }
    }
}
