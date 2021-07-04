using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Infrastructure.Profiles
{
    public class ProgressCourseProfile : Profile
    {
        public ProgressCourseProfile()
        {
            CreateMap<Tuple<Course,IEnumerable<int>>,CourseProgressDTO>()
             .ForMember(i => i.MaterialsNotPassed, j => j.MapFrom(k => k.Item1.Materials.Where(m => !k.Item2.Contains(m.Id)).ToList()))
             .ForMember(i => i.MaterialsPassed, j => j.MapFrom(k => k.Item1.Materials.Where(m => k.Item2.Contains(m.Id)).ToList()))
             .ForMember(i => i.Skills, j => j.MapFrom(k => k.Item1.Skills))
             .ForMember(i => i.Name, j => j.MapFrom(k => k.Item1.Name))
             .ForMember(i => i.Id, j => j.MapFrom(k => k.Item1.Id))
             .ForMember(i => i.Description, j => j.MapFrom(k => k.Item1.Description));
        }
    }
}
