using Application.DTO;
using AutoMapper;
using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Infrastructure.Profiles
{
    class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDTO, CourseViewModel>();
            CreateMap<CourseViewModel, CourseDTO>();

            CreateMap<CourseProgressDTO, CourseProgressViewModel>();
            CreateMap<CourseProgressViewModel, CourseProgressDTO>();
        }
    }
}
