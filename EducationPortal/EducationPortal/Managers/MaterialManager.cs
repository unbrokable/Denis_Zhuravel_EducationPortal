using AutoMapper;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EducationPortal.Interfaces;
using EducationPortal.Models.MaterialsViewModels;
using FluentValidation;
using EducationPortal.Models.Validators;

namespace EducationPortal.Controllers
{
    class MaterialManager: IMaterialManager
    {
        private readonly IServiceEntities<MaterialDTO> service;
        private readonly IAutoMapperUlConfiguration mapper;
        private readonly ICommand<MaterialViewModel> commandCreateMaterial;

        public MaterialManager(IServiceEntities<MaterialDTO> service, IAutoMapperUlConfiguration mapper, ICommand<MaterialViewModel> command)
        {
            this.service = service;
            this.mapper = mapper;
            this.commandCreateMaterial = command;
        }

        public void CreateMaterial(int userId)
        {
            MaterialViewModel material;

            try
            {
                material = commandCreateMaterial.Execute(userId);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid data");
                return;
            }
            
            if(material != null)
            {
                service.Create(mapper.GetMapper().Map<MaterialViewModel, MaterialDTO>(material));
            }
        }

        public IEnumerable<MaterialViewModel> ShowAvaibleMaterial(int id)
        {
            var materials = service.GetAllBy(i => i.CreatorId == id).ToList();
            var materialsview = mapper.GetMapper().Map<List<MaterialDTO>, List<MaterialViewModel>>(materials);
            return materialsview;
        }
    }
}
