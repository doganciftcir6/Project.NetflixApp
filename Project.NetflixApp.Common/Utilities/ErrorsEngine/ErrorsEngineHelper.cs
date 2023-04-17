using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Common.Utilities.ErrorsEngine
{
    //buraya dikkat bu engine hata mesajlarını birleştirerek geri döndürür. Hataları business veya controller tarafında ayırmak gerekli.
    public static class ErrorsEngineHelper
    {
        public static IResponse Run(params IResponse[] logics)
        {
            var errorMessages = logics.Where(x => x.ResponseType != ResponseType.Success)
                                      .Select(x => x.Message)
                                      .ToList();

            if (errorMessages.Any())
            {
                // Hata mesajlarını birleştirerek ErrorResponse döndür
                var errorMessage = string.Join("", errorMessages);
                return new Response(ResponseType.Error, errorMessage);
            }
            return new Response(ResponseType.Success);
        }
    }
}
