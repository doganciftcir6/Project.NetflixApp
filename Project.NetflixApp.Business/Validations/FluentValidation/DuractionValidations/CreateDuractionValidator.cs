using FluentValidation;
using Project.NetflixApp.Dtos.DuractionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.DuractionValidations
{
    public class CreateDuractionValidator : AbstractValidator<CreateDuractionDto>
    {
        public CreateDuractionValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description field cannot be empty!");
            RuleFor(x => x.Description).MaximumLength(200).WithMessage("Description field can be a max of 200 characters!");
            RuleFor(x => x.Description).MinimumLength(3).WithMessage("Description field must be a min of 3 characters!");
        }
    }
}
