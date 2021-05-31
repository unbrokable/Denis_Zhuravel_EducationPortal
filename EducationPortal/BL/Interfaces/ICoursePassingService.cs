using Application.DTO;
using Application.DTO.MaterialDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICoursePassingService
    {
        public CourseProgressDTO GetProgressCourse(int idUser,int idCourse);
        public MaterialDTO PassMaterial(int idUser, int idCourse, int idMaterial);
        public IEnumerable<CourseProgressDTO> GetProgressCourses(int idUser);
        public IEnumerable<CourseDTO> GetCourses(Predicate<CourseDTO> predicate);
        public bool ChooseCourse(int idUser, int idCourse);
    }
}
