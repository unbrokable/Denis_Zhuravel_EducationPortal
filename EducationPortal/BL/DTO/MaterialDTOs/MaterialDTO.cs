using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.MaterialDTOs
{
    public abstract class MaterialDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public int CreatorId { get; set; }
        public UserDTO Creator { get; set; }

    }
}
