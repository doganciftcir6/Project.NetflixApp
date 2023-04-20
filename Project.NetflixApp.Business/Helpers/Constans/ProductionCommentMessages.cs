using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class ProductionCommentMessages
    {
        public static string Deleted = "The productioncomment was successfully deleted";
        public static string NotDeleted = "The productioncomment parameter could not be deleted because the productioncomment could not be found.";
        public static string NotFound = "The related productioncomment could not be found.Productioncomment Id: ";
        public static string Created = "The productioncomment adding process has been successfully completed.";
        public static string Updated = "The productioncomment updating process has been successfully completed.";
        public static string NotUpdated = "The related productioncomment could not be found. So the update process could not be completed. Productioncomment Id: ";
    }
}
