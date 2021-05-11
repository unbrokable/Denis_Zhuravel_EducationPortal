using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface IService<T>
    {
        void Create(T data);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
