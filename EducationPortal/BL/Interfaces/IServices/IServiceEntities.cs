using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceEntities<T>
    {
        Task CreateAsync(T data);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync(int amount);
    }
}
