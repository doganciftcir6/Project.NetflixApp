using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class ProductionMessages
    {
        public static string Deleted = "The production was successfully deleted";
        public static string NotDeleted = "The production parameter could not be deleted because the production could not be found.";
        public static string NotFound = "The related production could not be found. Production Id: ";
        public static string Created = "The production adding process has been successfully completed.";
        public static string Updated = "The production updating process has been successfully completed.";
        public static string NotUpdated = "The related production could not be found. So the update process could not be completed. Production Id: ";
    }
}
