﻿using Project.NetflixApp.Common.Utilities.Results.Abstract;
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
        Task CreateUserAsync(RegisterUserDto registerUserDto);
        Task<IResponse> UpdateAsync(UpdateUserDto updateUserDto);
        Task<IResponse> DeleteAsync(int id);
        Task<IDataResponse<IEnumerable<GetUserDto>>> GetAllAsync();
        Task<IDataResponse<GetUserDto>> GetByIdAsync(int id);
        Task<IDataResponse<GetUserDto>> GetByEmailAsync(string email);
        Task<IResponse> UserEmailExistAsync(string email);
    }
}
