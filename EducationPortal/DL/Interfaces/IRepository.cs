using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Interfaces
{
   public  interface IRepository<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Create(T data);
       // bool Update();
    }
}
