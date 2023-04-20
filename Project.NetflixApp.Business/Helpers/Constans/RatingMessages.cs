using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class RatingMessages
    {
        public static string Deleted = "The rating was successfully deleted";
        public static string NotDeleted = "The rating parameter could not be deleted because the rating could not be found.";
        public static string NotFound = "The related rating could not be found. Rating Id: ";
        public static string Created = "The rating adding process has been successfully completed.";
        public static string Updated = "The rating adding process has been successfully completed.";
        public static string NotUpdated = "The related rating could not be found. So the update process could not be completed. Rating Id: ";
    }
}
