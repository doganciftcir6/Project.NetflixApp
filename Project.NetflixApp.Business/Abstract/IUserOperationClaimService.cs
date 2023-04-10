using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.UserOperationClaimDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IResponse> InsertAsync(CreateUserOperationClaimDto createUserOperationClaimDto);
        Task<IResponse> UpdateAsync(UpdateUserOperationClaimDto updateUserOperationClaimDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetUserOperationClaimDto>>> GetAllAsync();
        Task<IDataResponse<GetUserOperationClaimDto>> GetByIdAsync(int id);
    }
}
