using EducationPortal.Interfaces;
using EducationPortal.Models;
using EducationPortal.Models.MaterialsViewModels;
using EducationPortal.Models.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Managers.Commands
{
    class BookMaterialCreateCommand : ICommandMaterial
    {
        public MaterialViewModel Execute(string name, string location, int idUser)
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
        public override string ToString() => "Book material";
    }
}
