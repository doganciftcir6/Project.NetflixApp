using Microsoft.AspNetCore.Http;
using Project.NetflixApp.Business.Helpers.Constans;
using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.UserUploadHelpers
{
    public static class UserUploadSecurityHelper
    {
        public static IResponse CheckIfImageExtensionsAllow(string fileName)
        {
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> allowFileExtensions = new List<string>() { ".jpg", ".jpeg", ".gif", ".png" };

            if (!allowFileExtensions.Contains(extension))
            {
                return new Response(ResponseType.Error, UserMessages.NotCheckIfImageExtensions);
            }
            return new Response(ResponseType.Success);
        }
        public static IResponse CheckIfImageSizeIsLessThanOneMb(long imageSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imageSize * 0.000001);
            if (imgMbSize > 1)
            {
                return new Response(ResponseType.Error, UserMessages.NotCheckIfImageSizeIsLessThanOneMb);
            }
            return new Response(ResponseType.Success);
        }
        public static IResponse CheckImageName(string fileName)
        {
            if (fileName.Contains("/") || fileName.Contains("<") || fileName.Contains(">") || fileName.Contains("%2F") || fileName.Contains("%5C"))
            {
                return new Response(ResponseType.Error, UserMessages.NotCheckImageName);
            }
            return new Response(ResponseType.Success);
        }
        public static IResponse CheckImageNameDot(IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            if (fileName.Contains("."))
            {
                return new Response(ResponseType.Error, UserMessages.NotCheckImageNameDot);
            }
            return new Response(ResponseType.Success);
        }
    }
}
