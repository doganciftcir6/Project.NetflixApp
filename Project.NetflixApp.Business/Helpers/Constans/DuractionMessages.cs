using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class DuractionMessages
    {
        public static string Deleted = "The duraction was successfully deleted";
        public static string NotDeleted = "The duraction parameter could not be deleted because the duraction could not be found.";
        public static string NotFound = "The related duraction could not be found. Duraction Id: ";
        public static string Created = "The duraction adding process has been successfully completed.";
        public static string Updated = "The duraction updating process has been successfully completed.";
        public static string NotUpdated = "The related duraction could not be found. So the update process could not be completed. Duraction Id: ";
    }
}
