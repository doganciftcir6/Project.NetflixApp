using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class ProductionCategoryMessages
    {
        public static string Deleted = "The productioncategory was successfully deleted";
        public static string NotDeleted = "The productioncategory parameter could not be deleted because the productioncategory could not be found.";
        public static string NotFound = "The related productioncategory could not be found. Productioncategory Id: ";
        public static string Created = "The productioncategory adding process has been successfully completed.";
        public static string Updated = "The productioncategory updating process has been successfully completed.";
        public static string NotUpdated = "The related productioncategory could not be found. So the update process could not be completed. Productioncategory Id: ";
    }
}
