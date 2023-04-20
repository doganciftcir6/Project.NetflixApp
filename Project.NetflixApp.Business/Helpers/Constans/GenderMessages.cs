using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class GenderMessages
    {
        public static string Deleted = "The gender was successfully deleted";
        public static string NotDeleted = "The gender parameter could not be deleted because the gender could not be found.";
        public static string NotFound = "The related gender could not be found. Gender Id: ";
        public static string Created = "The gender adding process has been successfully completed.";
        public static string Updated = "The gender updating process has been successfully completed.";
        public static string NotUpdated = "The related gender could not be found. So the update process could not be completed. Gender Id: ";
    }
}
