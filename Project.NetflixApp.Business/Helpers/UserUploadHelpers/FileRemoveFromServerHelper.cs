using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.UserUploadHelpers
{
    public class FileRemoveFromServerHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileRemoveFromServerHelper(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void DeleteFileRun(string file)
        {
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var extName = Path.GetExtension(file);
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "UserImage", fileName + extName);
                if (path.Contains(fileName))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
