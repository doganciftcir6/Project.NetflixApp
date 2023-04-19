using Microsoft.IdentityModel.Tokens;
using Project.NetflixApp.Dtos.TokenDtos;
using Project.NetflixApp.Dtos.UserDtos;
using Project.NetflixApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.NetflixApp.Common.Utilities.Security.JWT
{
    public static class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(GetUserDto user, List<OperationClaim> operationClaims)
        {
            //Security Key'in simetriğini alalım
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            //ExpireDate oluşturalım (token geçerlilik süresi)
            var expireDate = DateTime.UtcNow.AddMinutes(1);
            //Şifrelenmiş kimliği oluşturuyoruz
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //token oluşurken token bilgisinin içerisinde kullanıcının rolü ve adı ve nameIdentifier ve email de olsun.
            List<Claim> myClaims = new List<Claim>();
            if (operationClaims.Count > 0)
            {
                foreach (var claim in operationClaims)
                {
                    myClaims.Add(new Claim(ClaimTypes.Role, claim.Description));
                }
            }
            if (user.Id != null)
            {
                myClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            }
            if (!string.IsNullOrEmpty(user.Name))
            {
                myClaims.Add(new Claim(ClaimTypes.Name, user.Name));
            }
            if (!string.IsNullOrEmpty(user.Email))
            {
                myClaims.Add(new Claim("Email", user.Email));
            }
            //Token ayarlarını yapıyoruz
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: myClaims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: credentials);

            //Token oluşturucu sınıfından bir örnek alalım
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //Token üretelim
            //return handler.WriteToken(token);
            return new TokenResponseDto(handler.WriteToken(token), expireDate);

        }
    }
}
