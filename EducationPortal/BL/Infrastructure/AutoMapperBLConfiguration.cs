using AutoMapper;
using Application.DTO;
using Application.Interfaces;
using Domain;

namespace Application
{
    public class AutoMapperBLConfiguration: IAutoMapperBLConfiguration
    {
        public IMapper CreateMapper()
        {
            return new MapperConfiguration(
            i =>
            {
                i.CreateMap<User, UserDTO>();
                i.CreateMap<UserDTO,User>();

            }).CreateMapper();

        }   
    }
}
