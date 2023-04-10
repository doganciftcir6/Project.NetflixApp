using Project.NetflixApp.Common.Utilities.Results.Abstract;
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
        Task<IResponse> InsertAsync(CreateOperationClaimDto operationClaimDto);
        Task<IResponse> UpdateAsync(UpdateOperationClaimDto operationClaimDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetOperationClaimDto>>> GetAllAsync();
        Task<IDataResponse<GetOperationClaimDto>> GetByIdAsync(int id);
    }
}
