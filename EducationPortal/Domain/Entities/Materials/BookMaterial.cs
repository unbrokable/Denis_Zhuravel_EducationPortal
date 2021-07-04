using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BookMaterial:Material
    {
        public string Author { get; set; }
        public int AmountOfPages { get; set; }
        public string Format { get; set; }
        public DateTime DateOfPublished { get; set; }
    }
}
