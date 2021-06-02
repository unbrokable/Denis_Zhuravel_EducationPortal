using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface IMaterialManager
    {
        void CreateMaterial(int userId);
        IEnumerable<MaterialViewModel> ShowAvaibleMaterial(int userId);
    }
}
