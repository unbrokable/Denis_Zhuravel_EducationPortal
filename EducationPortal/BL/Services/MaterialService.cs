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
        private readonly IEntitiesRepository repository;
        private readonly IAutoMapperBLConfiguration mapper;
            
        public MaterialService(IEntitiesRepository repository,IAutoMapperBLConfiguration mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public void Create(MaterialDTO data)
        {
            var material = mapper.GetMapper().Map<MaterialDTO,Material>(data);
            if(material != null)
                repository.Create<Material>(material);
        }

        public MaterialDTO GetById(int id)
        {
            return mapper.GetMapper().Map<Material, MaterialDTO>(repository.GetBy<Material>(i => i.Id == id));
        }

        public MaterialDTO GetBy(Predicate<MaterialDTO> predicate)
        {
            return mapper.GetMapper().Map<Material, MaterialDTO>(repository.GetBy<Material>(i => PredicateTranform(i,predicate)));
        }

        public IEnumerable<MaterialDTO> GetAllBy(Predicate<MaterialDTO> predicate)
        {
            return mapper.GetMapper().Map<IEnumerable<Material>, IEnumerable<MaterialDTO>>(repository.GetAllBy<Material>(i => PredicateTranform(i, predicate)))
               .ToList();
        }
        bool PredicateTranform(Material material, Predicate<MaterialDTO> predicate)
        {
            return predicate(mapper.GetMapper().Map<Material, MaterialDTO>(material));
        }
    }
}
