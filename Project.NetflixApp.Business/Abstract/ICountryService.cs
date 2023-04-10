using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.CountryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface ICountryService
    {
        Task<IResponse> InsertAsync(CreateCountryDto createCountryDto);
        Task<IResponse> UpdateAsync(UpdateCountryDto updateCountryDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetCountryDto>>> GetAllAsync();
        Task<IDataResponse<GetCountryDto>> GetByIdAsync(int id);
    }
}
