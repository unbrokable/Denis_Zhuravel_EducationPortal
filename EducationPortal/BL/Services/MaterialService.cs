using Application.DTO.CheckDTOs;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Specification;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Entities;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MaterialService : IServiceMaterial
    {
        private readonly IEntitiesRepository repository;
        private readonly IAutoMapperBLConfiguration mapper;
            
        public MaterialService(IEntitiesRepository repository,IAutoMapperBLConfiguration mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(MaterialDTO data)
        {
            var material = mapper.GetMapper().Map<MaterialDTO,Material>(data);
            if(material != null)
            {   
               await repository.AddAsync<Material>(material);
                return;
            }
            throw new ArgumentException("Cant create material");

        }

        // change after mvc on page
        public async Task<IEnumerable<MaterialDTO>> GetAsync(int amount)
        {
            var page = await repository.GetAsync<Material>(0, amount);
            // change on page
            return mapper.GetMapper().Map<IEnumerable<Material>, IEnumerable<MaterialDTO>>(page.Items);
        }

        public async Task<MaterialDTO> GetByIdAsync(int id)
        {
            return mapper.GetMapper()
                .Map<Material, MaterialDTO>(await repository.FindAsync<Material>(MaterialSpecification.FilterById(id)));
        }

        public async Task<IEnumerable<MaterialDTO>> GetMaterialOfCreatorAsync(int userId)
        {
            return mapper.GetMapper().Map<IEnumerable<Material>, IEnumerable<MaterialDTO>>( await repository.GetAsync<Material>(MaterialSpecification.FilterByCreator(userId)))
               .ToList();
        }

        public async Task Remove( int id)
        {
            var check =  (await repository.GetCustomSelectAsync<Material, MateriaCheckDTO>(MaterialSpecification.FilterById(id), mapper.GetMapper().ConfigurationProvider, i => i.Courses, i => i.Users)).FirstOrDefault();

            if (check.CourseAmount > 0 || check.UsersAmount > 0 )
            {
                throw new ArgumentException("Cant delete this material");
            }
            await repository.RemoveAsync<Material>(id);
        }

    }
}
