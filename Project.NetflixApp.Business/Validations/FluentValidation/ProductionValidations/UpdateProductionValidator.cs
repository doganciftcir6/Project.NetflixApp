using FluentValidation;
using Project.NetflixApp.Dtos.ProductionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Validations.FluentValidation.ProductionValidations
{
    public class UpdateProductionValidator : AbstractValidator<UpdateProductionDto>
    {
        public UpdateProductionValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id field cannot be empty!");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title field cannot be empty!");
            RuleFor(x => x.Title).MaximumLength(500).WithMessage("Title field can be a max of 500 characters!");
            RuleFor(x => x.Title).MinimumLength(3).WithMessage("Title field must be a min of 3 characters!");
            RuleFor(x => x.Director).NotEmpty().WithMessage("Director field cannot be empty!");
            RuleFor(x => x.Director).MaximumLength(500).WithMessage("Director field can be a max of 500 characters!");
            RuleFor(x => x.Director).MinimumLength(3).WithMessage("Director field must be a min of 3 characters!");
            RuleFor(x => x.Cast).NotEmpty().WithMessage("Cast field cannot be empty!");
            RuleFor(x => x.Cast).MaximumLength(1000).WithMessage("Cast field can be a max of 1000 characters!");
            RuleFor(x => x.Cast).MinimumLength(3).WithMessage("Cast field must be a min of 3 characters!");
            RuleFor(x => x.ReleaseYear).NotEmpty().WithMessage("ReleaseYear field cannot be empty!");
            RuleFor(x => x.ReleaseYear).MaximumLength(300).WithMessage("ReleaseYear field can be a max of 300 characters!");
            RuleFor(x => x.ReleaseYear).MinimumLength(3).WithMessage("ReleaseYear field must be a min of 3 characters!");
            RuleFor(x => x.TypeEntityId).NotEmpty().WithMessage("TypeEntityId field cannot be empty!");
            RuleFor(x => x.CountryId).NotEmpty().WithMessage("CountryId field cannot be empty!");
            RuleFor(x => x.RatingId).NotEmpty().WithMessage("RatingId field cannot be empty!");
            RuleFor(x => x.DuractionId).NotEmpty().WithMessage("DuractionId field cannot be empty!");
        }
    }
}
