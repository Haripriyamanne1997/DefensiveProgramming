using Microsoft.AspNetCore.Mvc;

namespace DefensiveProgramming.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LargeFilesWithValidationController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required");

            if (file.Length > 5 * 1024 * 1024) // 5MB limit
                return BadRequest("File size exceeds the 5MB limit");

            var allowedExtensions = new[] { ".jpg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName)?.ToLower();

            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
                return BadRequest("Invalid file type. Only .jpg, .png, and .pdf are allowed");

            // Ensure the uploads directory exists
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Prevent path traversal and generate a unique filename
            var safeFileName = Path.GetFileName(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { Message = "File uploaded successfully", FileName = uniqueFileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
