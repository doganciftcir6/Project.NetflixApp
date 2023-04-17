using Project.NetflixApp.Common.Enums;
using Project.NetflixApp.Common.Utilities.Results.Abstract;
using Project.NetflixApp.Common.Utilities.Results.Concrete;
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
                //şifremi hashle burda saltla birleşiyor aslında.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //login işlemi için yeni bir metot
        public static IResponse VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //artık elimde bir passwordsalt var o yüzden buraya girelim onu. Salt kontrolü burda yapılıyor.
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //passwordhash oluşturalım
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //karakter karakter kontrol işlemi yapıcaz. 
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                    {
                        return new Response(ResponseType.Error);
                    }
                }
            }
            return new Response(ResponseType.Success);
        }
    }
}
