using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spotify_authentication.Interface;
using spotify_authentication.Model;

namespace spotify_authentication.Controllers
{
    public class TokenController : Controller
    {
        private readonly IJWTTokenManager _tokenManager;

        public TokenController(IJWTTokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }
        [HttpPost("authenticate")]
        public IActionResult Authentication([FromBody] User user)
        {
            var token = _tokenManager.Authenticate(user.userName, user.password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet("authorization")]
        [Authorize]
        public IActionResult Authorization()
        {
            return Ok();
        }
    }
}
