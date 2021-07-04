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
using Application.Interfaces.IServices;
using System.Threading.Tasks;

namespace EducationPortal.Controllers
{
    class MaterialManager: IMaterialManager
    {
        private readonly IServiceMaterial service;
        private readonly IAutoMapperUlConfiguration mapper;
        private readonly ICommand<MaterialViewModel> commandCreateMaterial;

        public MaterialManager(IServiceMaterial service, IAutoMapperUlConfiguration mapper, ICommand<MaterialViewModel> command)
        {
            this.service = service;
            this.mapper = mapper;
            this.commandCreateMaterial = command;
        }

        public async Task CreateMaterialAsync(int userId)
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
               await service.CreateAsync(mapper.GetMapper().Map<MaterialViewModel, MaterialDTO>(material));
            }
        }

        public async Task Remove(int creatorId)
        {
            foreach (var item in await ShowAvaibleMaterialAsync(creatorId))
            {
                Console.WriteLine(item.ToString());
            } 
            Console.WriteLine("Enter id of material");
            try
            {
                await service.Remove(Convert.ToInt32(Console.ReadLine()));
            }
            catch (ArgumentException ex) 
            {
                Console.WriteLine($"Error {ex.Message}");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid data");
            }
            
        }

        public async Task<IEnumerable<MaterialViewModel>> ShowAvaibleMaterialAsync(int creatorId)
        {
            var materials = await service.GetMaterialOfCreatorAsync(creatorId);
            var materialsview = mapper.GetMapper().Map<IEnumerable<MaterialDTO>, IEnumerable<MaterialViewModel>>(materials);
            return materialsview;
        }
    }
}
