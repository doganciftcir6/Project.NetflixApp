using FluentValidation;
using Project.NetflixApp.Dtos.ProductionCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.ProductionCategoryValidator
{
    public class CreateProductionCategoryValidator : AbstractValidator<CreateProductionCategoryDto>
    {
        public CreateProductionCategoryValidator()
        {
            RuleFor(x => x.ProductionId).NotEmpty().WithMessage("ProductionId field cannot be empty!");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId field cannot be empty!");
        }
    }
}
