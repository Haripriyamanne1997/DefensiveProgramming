using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DefensiveProgramming.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AuthorizeFileController : ControllerBase
    {
        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return BadRequest("User not authenticated");

            var userFolder = Path.Combine("Uploads", userId); // Restrict access to user-specific files
            var safeFileName = Path.GetFileName(filename); // Prevent path traversal attacks
            var filePath = Path.Combine(userFolder, safeFileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", safeFileName);
        }
    }
}
