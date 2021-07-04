using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models.MaterialsViewModels
{
    class ArticleMaterialViewModel : MaterialViewModel
    {
        public DateTime DateOfPublished { get; set; }
        public override string ToString()
        {
            StringBuilder @string = new StringBuilder();
            @string.Append("Article material: ")
                .Append(base.ToString())
                .Append($" Date of Published {DateOfPublished.ToShortDateString()}\n");
            return @string.ToString();
        }
    }
}
