using Application.DTO;

namespace Application.Interfaces
{
    public interface IServiceUser : IServiceEntities<UserDTO>
    {
        bool Create(string name, string email, string password, string password2);
        UserDTO Login(string password, string email);
        public bool ExistNameEmail(string name, string email);
    }
}
