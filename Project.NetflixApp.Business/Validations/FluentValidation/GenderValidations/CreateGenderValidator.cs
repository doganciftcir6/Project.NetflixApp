using FluentValidation;
using Project.NetflixApp.Dtos.GenderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.GenderValidations
{
    public class CreateGenderValidator : AbstractValidator<CreateGenderDto>
    {
        public CreateGenderValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition field cannot be empty!");
            RuleFor(x => x.Definition).MaximumLength(100).WithMessage("Definition field can be a max of 100 characters!");
            RuleFor(x => x.Definition).MinimumLength(3).WithMessage("Definition field must be a min of 3 characters!");
        }
    }
}
