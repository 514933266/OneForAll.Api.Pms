﻿using AutoMapper;
using Pms.Application.Dtos;
using Pms.Domain.AggregateRoots;
using Pms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pms.Host.Profiles
{
    public class PmsRiskProfile : Profile
    {
        public PmsRiskProfile()
        {
            CreateMap<PmsRisk, PmsRiskDto>();

            CreateMap<PmsRiskForm, PmsRisk>();
        }
    }
}
