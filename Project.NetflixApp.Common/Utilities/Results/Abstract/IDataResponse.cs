using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Common.Utilities.Results.Abstract
{
    public interface IDataResponse<T> : IResponse
    {
        T Data { get; set; }
    }
}
