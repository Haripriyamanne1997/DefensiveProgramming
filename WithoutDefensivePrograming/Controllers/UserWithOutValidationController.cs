using DefensivePrograming.Infrastructure;
using DefensivePrograming.Infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace WithoutDefensivePrograming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWithOutValidationController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UserWithOutValidationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public IActionResult Register(WithOutUsers withOutUsers)
        {
            _dbContext.WithOutUsers.Add(withOutUsers);
            _dbContext.SaveChanges();
            return Ok("User registered successfully!");
        }
    }
}
