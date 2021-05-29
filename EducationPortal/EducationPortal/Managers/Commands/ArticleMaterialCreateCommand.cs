using EducationPortal.Interfaces;
using EducationPortal.Models;
using EducationPortal.Models.MaterialsViewModels;
using EducationPortal.Models.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Managers.Commands
{
    class ArticleMaterialCreateCommand : ICommandMaterial
    {
        public MaterialViewModel Execute(string name, string location, int idUser)
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
        public override string ToString() => "Article material";
    }
}
