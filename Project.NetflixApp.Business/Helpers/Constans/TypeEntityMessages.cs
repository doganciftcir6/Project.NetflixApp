using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class TypeEntityMessages
    {
        public static string Deleted = "The typeEntity was successfully deleted";
        public static string NotDeleted = "The typeEntity parameter could not be deleted because the typeEntity could not be found.";
        public static string NotFound = "The related typeEntity could not be found. TypeEntity Id: ";
        public static string Created = "The typeEntity adding process has been successfully completed.";
        public static string Updated = "The typeEntity updating process has been successfully completed.";
        public static string NotUpdated = "The related typeEntity could not be found. So the update process could not be completed. TypeEntity Id: ";
    }
}
