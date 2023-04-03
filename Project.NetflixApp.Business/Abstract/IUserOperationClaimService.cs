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
        Task<CreateUserOperationClaimDto> InsertAsync(CreateUserOperationClaimDto createUserOperationClaimDto);
        Task<UpdateUserOperationClaimDto> UpdateAsync(UpdateUserOperationClaimDto updateUserOperationClaimDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetUserOperationClaimDto>> GetAllAsync();
        Task<GetUserOperationClaimDto> GetByIdAsync(int id);
    }
}
