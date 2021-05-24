using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
    public interface IAutoMapperBLConfiguration
    {
        IMapper CreateMapper();
    }
}
