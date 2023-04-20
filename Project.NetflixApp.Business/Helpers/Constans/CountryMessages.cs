using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class CountryMessages
    {
        public static string Deleted = "The country was successfully deleted";
        public static string NotDeleted = "The country parameter could not be deleted because the country could not be found.";
        public static string NotFound = "The related country could not be found. Country Id: ";
        public static string Created = "The country adding process has been successfully completed.";
        public static string Updated = "The country updating process has been successfully completed.";
        public static string NotUpdated = "The related country could not be found. So the update process could not be completed. Country Id: ";
    }
}
