using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Common.Utilities.Results.Concrete
{
    public class Response : IResponse
    {
        public string Message { get; set; }
        public ResponseType ResponseType { get; set; }
        public List<CustomValidationErrors> CustomValidationErrors { get; set; }

        public Response(ResponseType responseType)
        {
            ResponseType = responseType;
        }
        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Message = message;
        }
        public Response(ResponseType responseType, List<CustomValidationErrors> errors)
        {
            ResponseType = responseType;
            CustomValidationErrors = errors;
        }
    }
}
