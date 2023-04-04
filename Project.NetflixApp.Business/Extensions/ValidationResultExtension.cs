using FluentValidation.Results;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Extensions
{
    public static class ValidationResultExtension
    {
        public static List<CustomValidationErrors> ConvertToCustomValidationError(this ValidationResult result)
        {
            List<CustomValidationErrors> customValidationErrors = new List<CustomValidationErrors>();
            foreach (var error in result.Errors)
            {
                customValidationErrors.Add(new CustomValidationErrors()
                {
                    ErrorMessage = error.ErrorMessage,
                    PropertyName = error.PropertyName,
                });
            }
            return customValidationErrors;
        }
    }
}
