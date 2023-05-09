using Microsoft.AspNetCore.Http;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IUserService
    {
        Task CreateUserAsync(RegisterUserDto registerUserDto, int roleId);
        Task<IResponse> UpdateAsync(UpdateUserDto updateUserDto, IFormFile image);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetUserWithoutPasswordDto>>> GetAllAsync();
        Task<IDataResponse<IEnumerable<GetUserWithoutPasswordDto>>> GetAllWithGenderAsync();
        Task<IDataResponse<GetUserWithoutPasswordDto>> GetByIdAsync(int id);
        Task<List<OperationClaim>> GetUserOperationClaims(int userId);
        Task<IDataResponse<GetUserWithoutPasswordDto>> GetByIdWithGenderAsync(int id);
        Task<IDataResponse<GetUserDto>> GetByEmailAsync(string email);
        Task<IResponse> UserEmailExistAsync(string email);
    }
}
