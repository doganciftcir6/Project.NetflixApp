using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class CategoryMessages
    {
        public static string SuccessfullyDelete = "The category was successfully deleted";
        public static string NotDeleted = "The category parameter could not be deleted because the category could not be found.";
        public static string NotFound = "The related category could not be found. Category Id: ";
        public static string Created = "The category adding process has been successfully completed.";
        public static string Updated = "The category updating process has been successfully completed.";
        public static string NotUpdated = "The related category could not be found. So the update process could not be completed. Category Id: ";
    }
}
