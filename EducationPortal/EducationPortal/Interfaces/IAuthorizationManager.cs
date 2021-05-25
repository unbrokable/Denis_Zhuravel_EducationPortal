using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface IAuthorizationManager
    {
        void Enter();
        bool CreateAccount();
    }
}
