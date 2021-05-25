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
            @string.Append("Book material: ")
                .Append(base.ToString())
                .Append($"Author {Author} Amount of Pages {AmountOfPages} Format {Format}")
                .Append($" Date of Published {DateOfPublished.ToShortDateString()}\n");
            return @string.ToString();
        }
    }
}
