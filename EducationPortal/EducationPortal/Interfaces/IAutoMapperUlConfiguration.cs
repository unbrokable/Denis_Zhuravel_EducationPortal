using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface IAutoMapperUlConfiguration
    {
        IMapper CreateMapper();
    }
}
