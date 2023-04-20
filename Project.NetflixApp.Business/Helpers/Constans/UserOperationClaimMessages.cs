using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class UserOperationClaimMessages
    {
        public static string Deleted = "The useroperationclaim was successfully deleted";
        public static string NotDeleted = "The useroperationclaim parameter could not be deleted because the useroperationclaim could not be found.";
        public static string NotFound = "The related useroperationclaim could not be found. Useroperationclaim Id: ";
        public static string Created = "The useroperationclaim adding process has been successfully completed.";
        public static string Updated = "The useroperationclaim updating process has been successfully completed.";
        public static string NotUpdated = "The related useroperationcliam could not be found. So the update process could not be completed. Useroperationclaim Id: ";
    }
}
