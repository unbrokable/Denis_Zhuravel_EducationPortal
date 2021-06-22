using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Interfaces
{
    interface ISkillManager
    {
        Task CreateAsync();
        Task ShowAsync();
    }
}
