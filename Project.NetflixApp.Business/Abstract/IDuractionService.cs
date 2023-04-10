using Project.NetflixApp.Common.Utilities.Results.Abstract;
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
        Task<IResponse> InsertAsync(CreateDuractionDto createDuractionDto);
        Task<IResponse> UpdateAsync(UpdateDuractionDto updateDuractionDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetDuractionDto>>> GetAllAsync();
        Task<IDataResponse<GetDuractionDto>> GetByIdAsync(int id);
    }
}
