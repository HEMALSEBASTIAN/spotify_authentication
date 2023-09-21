using Microsoft.IdentityModel.Tokens;
using spotify_authentication.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace spotify_authentication.Implementation
{
    public class JWTTokenManager : IJWTTokenManager
    {
        private readonly IConfiguration _configuration;

        public JWTTokenManager(IConfiguration configuration)
        {
            _configuration= configuration;
        }
        public string? Authenticate(string email, string password)
        {
            if (string.Equals(email, "test@gmail.com") && string.Equals(password, "test"))
            {
                var key = _configuration.GetValue<string>("JwtConfig:key");
                var keyBytes = Encoding.ASCII.GetBytes(key);

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, email) }),
                    Expires = DateTime.UtcNow.AddSeconds(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            else
                return null;
        }
    }
}
