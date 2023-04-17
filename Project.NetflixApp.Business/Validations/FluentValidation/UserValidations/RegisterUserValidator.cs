using FluentValidation;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.UserValidations
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field cannot be empty!");
            RuleFor(x => x.Name).MaximumLength(300).WithMessage("Name field can be a max of 300 characters!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name field must be a min of 3 characters!");
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("Lastname field cannot be empty!");
            RuleFor(x => x.Lastname).MaximumLength(300).WithMessage("Lastname field can be a max of 300 characters!");
            RuleFor(x => x.Lastname).MinimumLength(3).WithMessage("Lastname field must be a min of 3 characters!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email field cannot be empty!");
            RuleFor(x => x.Email).MaximumLength(300).WithMessage("Email field can be a max of 300 characters!");
            RuleFor(x => x.Email).MinimumLength(3).WithMessage("Email field must be a min of 3 characters!");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre boş olamaz");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Password field must be a min of 6 characters!");
            RuleFor(x => x.Password).MaximumLength(20).WithMessage("Password field can be a max of 20 characters!");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("The password must contain at least 1 uppercase letter!");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("The password must contain at least 1 lowercase letter!");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("The password must contain at least 1 digit/number!");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("The password must contain at least 1 special character!");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("GenderId field cannot be empty!");
        }
    }
}
