using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Util
{
    public class JWTCore
    {
        /// <summary>
        /// Generate encrypt JWT syring
        /// </summary>
        /// <param name="jwtModel"></param>
        /// <returns></returns>
        public static string GenerateJWT(JWTModel jwtModel)
        {
            var dateTime = DateTime.UtcNow;
            string issuer = AppsettingConfig.app(new string[] { "Audience", "Issuer" });
            string audience = AppsettingConfig.app(new string[] { "Audience", "Audience" });
            string secret = AppsettingConfig.app(new string[] { "Audience", "Secret" });

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,jwtModel.UId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat
                    ,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf
                    ,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                //Expire time
                new Claim(JwtRegisteredClaimNames.Exp
                    ,$"{new DateTimeOffset(DateTime.Now.AddSeconds(6000)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss,issuer),
                new Claim(JwtRegisteredClaimNames.Aud,audience)
            };
            claims.AddRange(jwtModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            //choose encrapt algorithms
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: issuer,
                claims: claims,
                signingCredentials: credential);
            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);
            return encodedJwt;
        }

        /// <summary>
        /// Serialize jwtStr
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static JWTModel SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object();
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var jwtModel = new JWTModel
            {
                UId = Convert.ToInt32(jwtToken.Id),
                Role = role != null ? role.ToString() : ""
            };
            return jwtModel;
        }
    }
}
