using BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface IServiceUser : IService<UserDTO>
    {
        bool Create(string name, string email, string password, string password2);
        UserDTO Login(string password, string email);
    }
}
