using AutoMapper;
using BL.DTO;
using BL.Interfaces;
using DL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class AutoMapperBLConfiguration: IAutoMapperBLConfiguration
    {
        public IMapper CreateMapper()
        {
            return new MapperConfiguration(
            i =>
            {
                i.CreateMap<User, UserDTO>();
            }).CreateMapper();

        }   
    }
}
