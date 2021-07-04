using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Interfaces
{
    interface IMaterialManager
    {
        Task CreateMaterialAsync(int userId);
        Task<IEnumerable<MaterialViewModel>> ShowAvaibleMaterialAsync(int userId);
        Task Remove(int idUser);
    }
}
