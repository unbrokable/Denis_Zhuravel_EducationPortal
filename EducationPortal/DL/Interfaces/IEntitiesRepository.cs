using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Interfaces
{
   public  interface IEntitiesRepository
    {
        IEnumerable<T> GetAll<T>() where T : class;
        bool Create<T>(T data) where T : class;
        bool Update<T>(IEnumerable<T> data) where T : class;
    }
}

