using Microsoft.AspNetCore.Mvc;
using DefensiveProgramming.Authentication;

namespace DefensiveProgramming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("Generate-token")]
        public IActionResult GenerateToken(string userId)
        {
            var token = _jwtTokenGenerator.GenerateToken(userId);
            return Ok(new { Token = token });
        }
    }
}
