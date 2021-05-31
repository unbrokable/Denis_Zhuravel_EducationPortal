using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [Serializable]
    public class ArticleMaterial: Material
    {
        public DateTime DateOfPublished { get; set;} 
    }
}
