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
        Task<CreateCountryDto> InsertAsync(CreateCountryDto createCountryDto);
        Task<UpdateCountryDto> UpdateAsync(UpdateCountryDto updateCountryDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetCountryDto>> GetAllAsync();
        Task<GetCountryDto> GetByIdAsync(int id);
    }
}
