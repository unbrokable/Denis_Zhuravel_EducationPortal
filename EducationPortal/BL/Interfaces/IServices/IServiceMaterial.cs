using Application.DTO.MaterialDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IServiceMaterial: IServiceEntities<MaterialDTO>
    {
        Task<IEnumerable<MaterialDTO>> GetMaterialOfCreatorAsync(int userId);
        Task Remove(int id);
    }
}
