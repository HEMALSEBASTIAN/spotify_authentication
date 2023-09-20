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
        public string? Authenticate(string username, string password)
        {
            if (string.Equals(username, "test") && string.Equals(password, "test"))
            {
                var key = _configuration.GetValue<string>("JwtConfig:key");
                var keyBytes = Encoding.ASCII.GetBytes(key);

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, username) }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
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
