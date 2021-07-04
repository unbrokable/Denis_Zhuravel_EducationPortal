using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.MaterialDTOs
{
    public class BookMaterialDTO : MaterialDTO
    {
        public string Author { get; set; }
        public int AmountOfPages { get; set; }
        public string Format { get; set; }
        public DateTime DateOfPublished { get; set; }
    }
}
