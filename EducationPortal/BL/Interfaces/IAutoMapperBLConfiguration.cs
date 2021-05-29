using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAutoMapperBLConfiguration
    {
        IMapper GetMapper();
    }
}
