using Project.NetflixApp.Dtos.OperationClaimDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<CreateOperationClaimDto> InsertAsync(CreateOperationClaimDto operationClaimDto);
        Task<UpdateOperationClaimDto> UpdateAsync(UpdateOperationClaimDto operationClaimDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetOperationClaimDto>> GetAllAsync();
        Task<GetOperationClaimDto> GetByIdAsync(int id);
    }
}
