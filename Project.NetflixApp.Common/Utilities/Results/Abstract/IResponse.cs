using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Common.Utilities.Results.Abstract
{
    public interface IResponse
    {
        string Message { get; set; }
        ResponseType ResponseType { get; set; }
        List<CustomValidationErrors> CustomValidationErrors { get; set; }
    }
}
