using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.MaterialDTOs
{
    public class VideoMaterialDTO: MaterialDTO
    {
        public TimeSpan Length { get; set; }
        public ResolutionDTO Resolution { get; set; }
 
    }
    public class ResolutionDTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
