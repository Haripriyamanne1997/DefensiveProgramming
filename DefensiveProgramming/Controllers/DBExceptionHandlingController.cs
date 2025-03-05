using DefensivePrograming.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DefensiveProgramming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBExceptionHandlingController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<DBExceptionHandlingController> _logger;

        public DBExceptionHandlingController(AppDbContext dbContext, ILogger<DBExceptionHandlingController> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                if (user == null)
                    return NotFound(new { message = "User not found. Please check the ID." });

                return Ok(user);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while retrieving user with ID: {UserId}", id);
                return StatusCode(500, new { message = "A database error occurred. Please try again later." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving user with ID: {UserId}", id);
                return StatusCode(500, new { message = "An unexpected error occurred. Please contact support." });
            }
        }
    }
}
