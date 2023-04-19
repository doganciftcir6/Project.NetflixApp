using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.UserUploadHelpers
{
    public class UploadUserHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private static UploadUserHelper _instance;
        public UploadUserHelper(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        public static UploadUserHelper CreateInstance(IHostingEnvironment hostingEnvironment)
        {
            _instance = new UploadUserHelper(hostingEnvironment);
            return _instance;
        }

        public async Task Upload(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString();
            var extName = Path.GetExtension(file.FileName);
            string path = Path.Combine(_hostingEnvironment.WebRootPath, "UserImage", fileName + extName);
            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Close();
        }
    }
}
