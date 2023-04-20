using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Business.Helpers.Constans
{
    public static class UserMessages
    {
        public static string Deleted = "The user was successfully deleted";
        public static string NotDeleted = "The user parameter could not be deleted because the user could not be found.";
        public static string NotFound = "The related user could not be found. User Id: ";
        public static string NotFoundEmail = "No user with the related email address could be found.";
        public static string Updated = "The user updating process has been successfully completed.";
        public static string NotUpdated = "The related user could not be found. So the update process could not be completed. User Id: ";
        public static string NotCheckIfImageExtensions = "The image you have uploaded must be one of the following file types: .jpg, .jpeg, .gif, .png^";
        public static string NotCheckIfImageSizeIsLessThanOneMb = "The size of the image you have uploaded must be less than 1 MB^";
        public static string NotCheckImageName = "The name of the image you have uploaded cannot contain the characters /, <, >, %2F, %5C^";
        public static string NotCheckImageNameDot = "The name of the image you have uploaded cannot contain the character '.'^";
        public static string AlreadyUsedEmail = "This email address has already been used. Please use a different email address^";
    }
}
