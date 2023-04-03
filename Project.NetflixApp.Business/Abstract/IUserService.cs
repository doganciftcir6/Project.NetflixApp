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
        Task<CreateUserDto> InsertAsync(CreateUserDto createUserDto);
        Task<UpdateUserDto> UpdateAsync(UpdateUserDto updateUserDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<GetUserDto>> GetAllAsync();
        Task<GetUserDto> GetByIdAsync(int id);
    }
}
