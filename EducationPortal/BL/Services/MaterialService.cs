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

        public async Task<IEnumerable<MaterialDTO>> GetAsync(int amount)
        {
            return ( await repository.GetQueryAsync<Material>(new Specification<Material>(i => true)))
                .Take(amount)
                .ProjectTo<MaterialDTO>(mapper.GetMapper().ConfigurationProvider)
                .ToList();
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
            IQueryable<object> checkQuery = (await repository.GetQueryAsync<Course>(CourseSpecification.FilterByMaterial(id)))
                .Select(i => new { Type = "Course" })
                .Union((await repository.GetQueryAsync<CompositionPassedMaterial>(PassedMaterialSpecification.FilterByMaterialId(id)))
                .Select(i => new { Type = "User" })
                )
                .GroupBy(i => i.Type);
            if (checkQuery.Any())
            {
                throw new ArgumentException("Cant delete this material");
            }

            await repository.RemoveAsync<Material>(id);
        }

    }
}
