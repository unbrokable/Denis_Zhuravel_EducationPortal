using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Infrastructure.Profiles
{
    public class SkillProfile:Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillDTO, Skill>();
            CreateMap<Skill, SkillDTO>();

            CreateMap<CompositionSkillUser, SkillUserDTO>()
            .ForMember(i => i.Name, j => j.MapFrom(k => k.Skill.Name))
            .ForMember(i => i.Id, j => j.MapFrom(j => j.SkillId));
        }
    }
}
