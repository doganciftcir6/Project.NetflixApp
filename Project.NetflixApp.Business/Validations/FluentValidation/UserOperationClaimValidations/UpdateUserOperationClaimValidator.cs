using FluentValidation;
using Project.NetflixApp.Dtos.UserOperationClaimDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.UserOperationClaimValidations
{
    public class UpdateUserOperationClaimValidator : AbstractValidator<UpdateUserOperationClaimDto>
    {
        public UpdateUserOperationClaimValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id field cannot be empty!");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId field cannot be empty!");
            RuleFor(x => x.OperationClaimId).NotEmpty().WithMessage("OperationClaimId field cannot be empty!");
        }
    }
}
