using Application.DTO;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceUser : IServiceEntities<UserDTO>
    {
        Task<bool> CreateAsync(string name, string email, string password, string password2);
        Task<UserDTO> LoginAsync(string password, string email);
        Task<bool> ExistNameEmailAsync(string name, string email);
    }
}
