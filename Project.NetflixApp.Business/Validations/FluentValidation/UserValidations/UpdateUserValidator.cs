using FluentValidation;
using Project.NetflixApp.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.UserValidations
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id field cannot be empty!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name field cannot be empty!");
            RuleFor(x => x.Name).MaximumLength(300).WithMessage("Name field can be a max of 300 characters!");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name field must be a min of 3 characters!");
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("Lastname field cannot be empty!");
            RuleFor(x => x.Lastname).MaximumLength(300).WithMessage("Lastname field can be a max of 300 characters!");
            RuleFor(x => x.Lastname).MinimumLength(3).WithMessage("Lastname field must be a min of 3 characters!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email field cannot be empty!");
            RuleFor(x => x.Email).MaximumLength(300).WithMessage("Email field can be a max of 300 characters!");
            RuleFor(x => x.Email).MinimumLength(3).WithMessage("Email field must be a min of 3 characters!");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("GenderId field cannot be empty!");
        }
    }
}
