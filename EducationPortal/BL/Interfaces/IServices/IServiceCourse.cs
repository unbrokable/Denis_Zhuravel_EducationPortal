using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IServiceCourse:IServiceEntities<CourseDTO>
    {
        Task<IEnumerable<CourseDTO>> GetAllExceptChoosenAsync(int userId);
        Task<IEnumerable<CourseDTO>> GetCourseOfCreatorAsync(int userId);
        Task Remove(int id);
    }
}
