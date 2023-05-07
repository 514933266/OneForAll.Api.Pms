using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Aggregates;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pms.Host.Profiles
{
    public class PmsTaskProfile : Profile
    {
        public PmsTaskProfile()
        {
            CreateMap<PmsTask, PmsTaskDto>()
                .ForMember(t => t.ProjectId, a => a.MapFrom(e => e.PmsProjectId))
                .ForMember(t => t.RequirementId, a => a.MapFrom(e => e.PmsRequirementId));

            CreateMap<PmsTaskDetailAggregate, PmsTaskDetailDto>()
                .ForMember(t => t.Id, a => a.MapFrom(e => e.Contact.Id))
                .ForMember(t => t.EstimateHours, a => a.MapFrom(e => e.Contact.EstimateHours))
                .ForMember(t => t.ActualHours, a => a.MapFrom(e => e.Contact.ActualHours))
                .ForMember(t => t.StartTime, a => a.MapFrom(e => e.Contact.StartTime))
                .ForMember(t => t.Status, a => a.MapFrom(e => e.Contact.Status))
                .ForMember(t => t.UserId, a => a.MapFrom(e => e.Member.SysUserId))
                .ForMember(t => t.UserNickName, a => a.MapFrom(e => e.Member.Name))
                .ForMember(t => t.UserName, a => a.MapFrom(e => e.Member.UserName))
                .ForMember(t => t.UserJob, a => a.MapFrom(e => e.Member.Job))
                .ForMember(t => t.Records, a => a.MapFrom(e => e.Records));

            CreateMap<PmsTaskForm, PmsTask>()
                .ForMember(t => t.PmsRequirementId, a => a.MapFrom(e => e.RequirementId));
            CreateMap<PmsTaskChangeStatusForm, PmsTaskMemberContact>();

            CreateMap<PmsMemberTaskStatistics, PmsMemberTaskStatisticsDto>();
        }
    }
}
