using Project.NetflixApp.Common.Utilities.Results.Abstract;
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
        Task<IResponse> InsertAsync(CreateTypeEntityDto createTypeEntityDto);
        Task<IResponse> UpdateAsync(UpdateTypeEntityDto updateTypeEntityDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetTypeEntityDto>>> GetAllAsync();
        Task<IDataResponse<GetTypeEntityDto>> GetByIdAsync(int id);
    }
}
