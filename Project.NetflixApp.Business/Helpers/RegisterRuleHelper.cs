using Project.NetflixApp.Business.Abstract;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using Project.NetflixApp.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers
{
    public class RegisterRuleHelper
    {
        private readonly IUserService _userService;
        public static RegisterRuleHelper _instance;
        
        public RegisterRuleHelper(IUserService userService)
        {
            _userService = userService;
        }
        public static RegisterRuleHelper CreateInstance(IUserService userService)
        {
            _instance = new RegisterRuleHelper(userService);
            return _instance;
        }
        public async Task<IResponse> CheckEmailExists(string email)
        {
            var emailResponse = await _userService.UserEmailExistAsync(email);
            if(emailResponse.ResponseType == ResponseType.Error)
            {
                return new Response(ResponseType.Error, "This email address has already been used. Please use a different email address.");
            }
            return new Response(ResponseType.Success);
        }
    }
}
