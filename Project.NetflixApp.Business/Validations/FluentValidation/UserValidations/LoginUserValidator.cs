using FluentValidation;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.UserValidations
{
    public class LoginUserValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email field cannot be empty!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password field cannot be empty!");
        }
    }
}
