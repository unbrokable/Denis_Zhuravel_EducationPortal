using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models.MaterialsViewModels
{
    class VideoMaterialViewModel : MaterialViewModel
    {
        public TimeSpan Length { get; set; }
        public ResolutionViewModel Resolution { get; set; }

        public override string ToString()
        {
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("Video material: ")
                .AppendLine(base.ToString())
                .AppendLine($" Length {Length}\n Resolution {Resolution}\n");
            return @string.ToString();
        }
    }

    class ResolutionViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public override string ToString()
        {
            return $"{Width}x{Height}";
        }
    }
}
