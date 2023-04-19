using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Dtos.TokenDtos;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Abstract
{
    public interface IAuthService
    {
        Task<IResponse> RegisterAsync(RegisterUserDto registerUserDto);
        Task<IDataResponse<TokenResponseDto>> LoginAsync(LoginUserDto loginUserDto);
    }
}
