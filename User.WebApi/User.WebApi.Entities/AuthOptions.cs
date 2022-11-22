using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace User.WebApi.User.WebApi.Entities
{
    public class AuthOptions
    {
        public const string ISSUER = "MyWebApi";
        public const string AUDIENCE = "AuthClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
