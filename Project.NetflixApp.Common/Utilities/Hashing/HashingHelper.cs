using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Common.Utilities.Hashing
{
    //passwordu hashlemeyi sağlayacak bir helper
    public static class HashingHelper
    {
        //out burda metot içinde değişkenleri değiştirip geriye değişmiş verilerle gönderilmesini sağlıyor. void bir metot olmadığı için normalde bunu yapamayız.
        public static void CreatePassword(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                //şifremi hashle
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
