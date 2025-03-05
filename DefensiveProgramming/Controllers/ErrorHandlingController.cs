using DefensivePrograming.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DefensiveProgramming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorHandlingController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ErrorHandlingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                if (user == null)
                    return NotFound(new { message = "User not found" }); // Proper response

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", error = ex.Message });
            }
        }
    }
}
