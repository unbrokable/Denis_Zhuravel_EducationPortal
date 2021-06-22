using Application.DTO;
using AutoMapper;
using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Infrastructure.Profiles
{
    class SkillProfile: Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillDTO, SkillViewModel>();
            CreateMap<SkillViewModel, SkillDTO>();
        }
    }
}
