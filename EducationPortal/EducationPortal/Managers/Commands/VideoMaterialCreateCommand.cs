using EducationPortal.Interfaces;
using EducationPortal.Models;
using EducationPortal.Models.MaterialsViewModels;
using EducationPortal.Models.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Managers.Commands
{
    class VideoMaterialCreateCommand : ICommandMaterial
    {
        public MaterialViewModel Execute(string name, string location, int idUser)
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

        public override string ToString() => "Video material";
    }
}
