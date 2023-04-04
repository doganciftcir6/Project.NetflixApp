using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Common.Utilities.Results.Concrete
{
    public class DataResponse<T> : Response, IDataResponse<T>
    {
        public T Data { get; set; }

        public DataResponse(ResponseType responseType, T data) : base(responseType)
        {
            Data = data;
        }
        public DataResponse(ResponseType responseType, string message) : base(responseType, message)
        {
        }

        public DataResponse(ResponseType responseType, T data ,List<CustomValidationErrors> errors) : base(responseType)
        {
            Data = data;
            CustomValidationErrors = errors;
        }

    }
}
