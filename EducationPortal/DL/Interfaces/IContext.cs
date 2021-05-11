using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Interfaces
{
    public interface IContext<T>
    {
        bool Save(T data);
        T Load();

    }
}
