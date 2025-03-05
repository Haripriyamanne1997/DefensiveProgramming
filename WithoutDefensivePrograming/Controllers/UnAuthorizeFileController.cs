using Microsoft.AspNetCore.Mvc;

namespace WithoutDefensivePrograming.Controllers
{
    [Route("[controller]")]
    public class UnAuthorizeFileController : ControllerBase
    {
        /// <summary>
        /// No Authorization being done on downloading a file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet("download/{filename}")]
        public IActionResult DownloadFile(string filename)
        {
            string filePath = Path.Combine("Uploads", filename);

            if (System.IO.File.Exists(filePath))
            {
                return File(System.IO.File.ReadAllBytes(filePath), "application/octet-stream", filename);
            }
            return NotFound();
        }
    }


        
    }
