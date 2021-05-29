using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceUser : IServiceEntities<UserDTO>
    {
        bool Create(string name, string email, string password, string password2);
        UserDTO Login(string password, string email);
    }
}
