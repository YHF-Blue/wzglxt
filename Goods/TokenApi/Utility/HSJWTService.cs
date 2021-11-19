using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.UserDTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TokenApi.Utility
{
    /// <summary>
    /// 对称加密方式生成token
    /// </summary>
    public class HSJWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        public HSJWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// accountOutput  --是登录用户的信息，需要存到token中
        /// </summary>
        /// <param name="accountOutput"></param>
        /// <returns></returns>
        public string GetToken(UserOutput accountOutput)
        {
            Claim[] claims = new[]
            {
               new Claim("UserId", accountOutput.UserId),
               new Claim("UserName", accountOutput.UserName),
               new Claim("DepartmentId", accountOutput.DepartmentId.ToString()),
               new Claim("RoleId",accountOutput.RoleName),

            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["HS256SecurityKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            /**
                iss (issuer)：签发人
                exp (expiration time)：过期时间
                sub (subject)：主题
                aud (audience)：受众
                nbf (Not Before)：生效时间
                iat (Issued At)：签发时间
                jti (JWT ID)：编号
             * */
            var token = new JwtSecurityToken(
                issuer: _configuration["issuer"],//配置签发人
                audience: _configuration["audience"],//配置受众
                claims: claims,//配置用户信息
                expires: DateTime.Now.AddMinutes(20),//配置20分钟有效期
                signingCredentials: creds);//配置秘钥信息
            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
