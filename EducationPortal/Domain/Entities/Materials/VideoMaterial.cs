using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [Serializable]
    public class VideoMaterial : Material
    {
        public TimeSpan Length { get; set; }
        public Resolution Resolution { get; set; }
    }
    [Serializable]
    public class Resolution
    {
        public int Width { get; set; }
        public int  Height { get; set; }
    }
}
