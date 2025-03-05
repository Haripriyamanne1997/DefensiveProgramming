using Microsoft.AspNetCore.Mvc;

namespace WithoutDefensivePrograming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LargeFilesWithOutValidationController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var filePath = Path.Combine("uploads", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok("File uploaded");
        }
    }
}
