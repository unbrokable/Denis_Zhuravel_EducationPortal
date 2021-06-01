using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models.MaterialsViewModels
{
    class BookMaterialViewModel : MaterialViewModel
    {
        public string Author { get; set; }
        public int AmountOfPages { get; set; }
        public string Format { get; set; }
        public DateTime DateOfPublished { get; set; }

        public override string ToString()
        {
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("Book material: ")
                .AppendLine(base.ToString())
                .AppendLine($" Author {Author}\n Amount of Pages {AmountOfPages}\n Format {Format}")
                .AppendLine ($" Date of Published {DateOfPublished.ToShortDateString()}");
            return @string.ToString();
        }
    }
}
