using Application.DTO;
using Application.DTO.MaterialDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICoursePassingService
    {
        Task<CourseProgressDTO> GetProgressCourseAsync(int idUser,int idCourse);
        Task<MaterialDTO> PassMaterialAsync(int idUser, int idCourse, int idMaterial);
        Task<IEnumerable<CourseProgressDTO>> GetProgressCoursesAsync(int idUser);
        Task<bool> ChooseCourseAsync(int idUser, int idCourse);
    }
}
