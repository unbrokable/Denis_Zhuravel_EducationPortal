using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models
{
    class RegistrationModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set;}
    }
}
