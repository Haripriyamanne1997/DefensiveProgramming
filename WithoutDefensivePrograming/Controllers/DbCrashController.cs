using DefensivePrograming.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WithoutDefensivePrograming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbCrashController : Controller
    {

        private readonly AppDbContext _dbContext;

        public DbCrashController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _dbContext.Users.Find(id); // If DB is down, this will crash
            return Ok(user);
        }
    }
}
