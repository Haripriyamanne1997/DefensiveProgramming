using DefensivePrograming.Infrastructure.DataModels;
using DefensivePrograming.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DefensiveProgramming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWithValidationController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UserWithValidationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns validation errors
            }
            // Hash password before saving (never store plain-text passwords)
            user.Password = HashPassword(user.Password);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return Ok("User registered successfully!");
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

    }
}
