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
        Task<CreateGenderDto> InsertAsync(CreateGenderDto createGenderDto);
        Task<UpdateGenderDto> UpdateAsync(UpdateGenderDto updateGenderDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetGenderDto>> GetAllAsync();
        Task<GetGenderDto> GetByIdAsync(int id);
    }
}
