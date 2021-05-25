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
        readonly IServiceEntities<MaterialDTO> service;
        readonly IAutoMapperUlConfiguration mapper; 
       
        public MaterialManager(IServiceEntities<MaterialDTO> service, IAutoMapperUlConfiguration mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public void CreateMaterial(int userId)
        {
            Console.WriteLine("1 Video material 2 Article material 3 Book material");
            string answer = Console.ReadLine();
            MaterialViewModel material = null;
            switch (answer)
            { 
                case "1":
                    material =  CreateCommon(CreateVideo);
                    break;
                case "2":
                    material = CreateCommon(CreateArtile);
                    break;
                case "3":
                    material = CreateCommon(CreateBook);
                    break;
                default:
                    Console.WriteLine("Cant find command");
                    break;
            }
            if(material != null)
            {
                material.Id = new Random().Next(0, 1000);
                service.Create(mapper.CreateMapper().Map<MaterialViewModel, MaterialDTO>(material));
            }
            T CreateCommon<T>( Func<string, string,int,T> create)
            {
                Console.WriteLine("Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Location:");
                string location = Console.ReadLine();
                return create(name, location,userId);
            }
            
            BookMaterialViewModel CreateBook(string name, string location,int idUser)
            {
                var book = new BookMaterialViewModel()
                {
                    CreatorId = idUser,
                    Name = name,
                    Location = location
                };
                Console.WriteLine("Amount of pages");
                book.AmountOfPages = int.Parse(Console.ReadLine());
                Console.WriteLine("Author");
                book.Author = Console.ReadLine();
                Console.WriteLine("Format");
                book.Format = Console.ReadLine();
                Console.WriteLine("Date of published(01/10/2015)");
                book.DateOfPublished = DateTime.Parse(Console.ReadLine());
                var validator = new BookMaterialValidator();
                var validateResult = validator.Validate(book);
                if (!validateResult.IsValid)
                {
                    Console.WriteLine(validateResult.ToString(","));
                    return null;
                }
                return book;
            }

            ArticleMaterialViewModel CreateArtile(string name, string location, int idUser)
            {
                var article = new ArticleMaterialViewModel()
                {
                    Name = name,
                    Location = location,
                    CreatorId = idUser
                    
                };
                Console.WriteLine("Date of published");
                article.DateOfPublished = DateTime.Parse(Console.ReadLine());
                var validator = new ArticleMaterialValidator();
                var validateResult = validator.Validate(article);
                if (!validateResult.IsValid)
                {
                    Console.WriteLine(validateResult.ToString(","));
                    return null;
                }
                return article;
            }

            VideoMaterialViewModel CreateVideo(string name, string location, int idUser)
            {
                var video = new VideoMaterialViewModel()
                {
                    Name = name,
                    Location = location,
                    CreatorId = idUser
                    
                };
                Console.WriteLine("Resolution Hight Weight");
                video.Resolution = new ResolutionViewModel()
                {
                    Width = int.Parse(Console.ReadLine()),
                    Height = int.Parse(Console.ReadLine())
                };
                Console.WriteLine("Length hours minutes");
                int hours = int.Parse(Console.ReadLine());
                int minutes = int.Parse(Console.ReadLine());
                video.Length = new TimeSpan(hours, minutes, 0);

                var validator = new VideoMaterialValidator();
                var validateResult = validator.Validate(video);
                if (!validateResult.IsValid)
                {
                    Console.WriteLine(validateResult.ToString(","));
                    return null;
                }

                return video;
            }
        }

        public IEnumerable<MaterialViewModel> ShowAvaibleMaterial(int id)
        {
            var materials = service.GetAll().Where(i => i.CreatorId == id).ToList();
            var materialsview = mapper.CreateMapper().Map<List<MaterialDTO>, List<MaterialViewModel>>(materials);
            return materialsview;
        }
    }
}
