using Project.NetflixApp.Dtos.TypeEntityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface ITypeEntityService
    {
        Task<CreateTypeEntityDto> InsertAsync(CreateTypeEntityDto createTypeEntityDto);
        Task<UpdateTypeEntityDto> UpdateAsync(UpdateTypeEntityDto updateTypeEntityDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetTypeEntityDto>> GetAllAsync();
        Task<GetTypeEntityDto> GetByIdAsync(int id);
    }
}
