using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Application.Services
{
    public class MaterialService : IServiceEntities<MaterialDTO>
    {
        readonly IEntitiesRepository repository;
        readonly IAutoMapperBLConfiguration mapper;
            
        public MaterialService(IEntitiesRepository repository,IAutoMapperBLConfiguration mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void Create(MaterialDTO data)
        {
            var material = mapper.CreateMapper().Map<MaterialDTO,Material>(data);
            if(material != null)
                repository.Create<Material>(material);
        }

        public IEnumerable<MaterialDTO> GetAll()
        {
            List<MaterialDTO> materialsDTO = mapper
                .CreateMapper()
                .Map< IEnumerable<Material>,IEnumerable<MaterialDTO>>(repository.GetAll<Material>())
                .ToList();
            return materialsDTO;
        }

        public MaterialDTO GetById(int id)
        {
            return GetAll().FirstOrDefault(i => i.Id == id);
        }

        public MaterialDTO GetBy(Predicate<MaterialDTO> predicate)
        {
            return GetAll().FirstOrDefault(i => predicate(i));
        }

        public IEnumerable<MaterialDTO> GetByAll(Predicate<MaterialDTO> predicate)
        {
             return mapper.CreateMapper().Map<IEnumerable<Material>,IEnumerable<MaterialDTO>>(repository.GetAll<Material>())
                .ToList().Where(i => predicate(i));
           
        }
    }
}
