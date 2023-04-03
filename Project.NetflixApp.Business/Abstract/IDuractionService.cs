using Project.NetflixApp.Dtos.DuractionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IDuractionService
    {
        Task<CreateDuractionDto> InsertAsync(CreateDuractionDto createDuractionDto);
        Task<UpdateDuractionDto> UpdateAsync(UpdateDuractionDto updateDuractionDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetDuractionDto>> GetAllAsync();
        Task<GetDuractionDto> GetByIdAsync(int id);
    }
}
