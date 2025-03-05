using DefensivePrograming.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WithoutDefensivePrograming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnhandledExceptionController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UnhandledExceptionController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
                throw new Exception("Error");  // Unhandled exception

            return Ok(user);
        }
    }
}
