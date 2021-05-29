using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public  interface IEntitiesRepository
    {
        IEnumerable<T> GetAll<T>() where T : class;
        bool Create<T>(T data) where T : class;
        bool Update<T>(IEnumerable<T> data) where T : class;
        IEnumerable<T> GetAllBy<T>(Predicate<T> predicate) where T : class;
        T GetBy<T>(Predicate<T> predicate) where T : class;
    }
}

