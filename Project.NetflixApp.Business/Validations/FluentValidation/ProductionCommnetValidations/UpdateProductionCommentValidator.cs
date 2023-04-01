using FluentValidation;
using Project.NetflixApp.Dtos.ProductionCommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.ProductionCommnetConfigurations
{
    public class UpdateProductionCommentValidator : AbstractValidator<UpdateProductionCommentDto>
    {
        public UpdateProductionCommentValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id field cannot be empty!");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content field cannot be empty!");
            RuleFor(x => x.Content).MaximumLength(900).WithMessage("Content field can be a max of 900 characters!");
            RuleFor(x => x.Content).MinimumLength(3).WithMessage("Content field must be a min of 3 characters!");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId field cannot be empty!");
            RuleFor(x => x.ProductionId).NotEmpty().WithMessage("ProductionId field cannot be empty!");
        }
    }
}
