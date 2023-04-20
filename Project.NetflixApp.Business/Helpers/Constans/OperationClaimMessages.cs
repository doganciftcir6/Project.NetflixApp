using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class OperationClaimMessages
    {
        public static string Deleted = "The OperationClaim was successfully deleted.";
        public static string NotDeleted = "The operationclaim parameter could not be deleted because the operationclaim could not be found.";
        public static string NotFound = "The related operationclaim could not be found. Operationclaim Id: ";
        public static string Created = "The operationclaim adding process has been successfully completed.";
        public static string Updated = "The operationclaim updating process has been successfully completed.";
        public static string NotUpdated = "The related operationclaim could not be found. So the update process could not be completed. Operationclaim Id: ";
    }
}
