using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IServiceEntities<T>
    {
        void Create(T data);
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetBy(Predicate<T> predicate);
        IEnumerable<T> GetAllBy(Predicate<T> predicate);
    }
}
