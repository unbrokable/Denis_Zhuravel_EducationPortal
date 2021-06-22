using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Interfaces
{
    interface IAuthorizationManager
    {
        Task<UserViewModel> EnterAsync();
        Task<bool> CreateAccountAsync();
        Task<UserViewModel> GetUserAsync(int id);
    }
}
