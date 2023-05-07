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
    public class PmsBugProfile : Profile
    {
        public PmsBugProfile()
        {
            CreateMap<PmsBug, PmsBugDto>()
                .ForMember(t => t.UserId, a => a.MapFrom(e => e.SysUserId));
            CreateMap<PmsBugAggregate, PmsBugDto>()
                .ForMember(t => t.Id, a => a.MapFrom(e => e.Bug.Id))
                .ForMember(t => t.Title, a => a.MapFrom(e => e.Bug.Title))
                .ForMember(t => t.Type, a => a.MapFrom(e => e.Bug.Type))
                .ForMember(t => t.Status, a => a.MapFrom(e => e.Bug.Status))
                .ForMember(t => t.Level, a => a.MapFrom(e => e.Bug.Level))
                .ForMember(t => t.Content, a => a.MapFrom(e => e.Bug.Content))
                .ForMember(t => t.Priority, a => a.MapFrom(e => e.Bug.Priority))
                .ForMember(t => t.CreatorId, a => a.MapFrom(e => e.Bug.CreatorId))
                .ForMember(t => t.CreatorName, a => a.MapFrom(e => e.Bug.CreatorName))
                .ForMember(t => t.CreateTime, a => a.MapFrom(e => e.Bug.CreateTime))
                .ForMember(t => t.UserId, a => a.MapFrom(e => e.Member.SysUserId))
                .ForMember(t => t.UserJob, a => a.MapFrom(e => e.Member.Job))
                .ForMember(t => t.UserNickName, a => a.MapFrom(e => e.Member.Name))
                .ForMember(t => t.UserName, a => a.MapFrom(e => e.Member.UserName))
                .ForMember(t => t.UserJob, a => a.MapFrom(e => e.Member.Job));
            CreateMap<PmsBugForm, PmsBug>()
                .ForMember(t => t.SysUserId, a => a.MapFrom(e => e.UserId));
        }
    }
}
