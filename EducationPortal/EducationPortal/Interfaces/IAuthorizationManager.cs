using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface IAuthorizationManager
    {
        UserViewModel Enter();
        bool CreateAccount();
        UserViewModel GetUser(int id);
    }
}
