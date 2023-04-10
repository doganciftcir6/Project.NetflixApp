using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.GenderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IGenderService
    {
        Task<IResponse> InsertAsync(CreateGenderDto createGenderDto);
        Task<IResponse> UpdateAsync(UpdateGenderDto updateGenderDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetGenderDto>>> GetAllAsync();
        Task<IDataResponse<GetGenderDto>> GetByIdAsync(int id);
    }
}
