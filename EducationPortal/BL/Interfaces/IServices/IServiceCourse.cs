using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface IServiceCourse:IServiceEntities<CourseDTO>
    {
        IEnumerable<CourseDTO> GetAllExceptChoosen(int userId);
    }
}
