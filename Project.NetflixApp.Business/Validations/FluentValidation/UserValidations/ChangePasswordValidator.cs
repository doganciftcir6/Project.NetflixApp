using FluentValidation;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.UserValidations
{
    public class ChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(X => X.CurrentPassword).NotEmpty().WithMessage("CurrentPassword field cannot be empty!");
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("NewPassword field cannot be empty!");
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("NewPassword field must be a min of 6 characters!");
            RuleFor(x => x.NewPassword).MaximumLength(20).WithMessage("NewPassword field can be a max of 20 characters!");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("The NewPassword must contain at least 1 uppercase letter!");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("The NewPassword must contain at least 1 lowercase letter!");
            RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("The NewPassword must contain at least 1 digit/number!");
            RuleFor(p => p.NewPassword).Matches("[^a-zA-Z0-9]").WithMessage("The NewPassword must contain at least 1 special character!");
        }
    }
}
