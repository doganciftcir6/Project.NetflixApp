using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IUserService
    {
        Task<IResponse> InsertAsync(CreateUserDto createUserDto);
        Task<IResponse> RegisterAsync(RegisterUserDto registerUserDto);
        Task<IResponse> UpdateAsync(UpdateUserDto updateUserDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetUserDto>>> GetAllAsync();
        Task<IDataResponse<GetUserDto>> GetByIdAsync(int id);
    }
}
