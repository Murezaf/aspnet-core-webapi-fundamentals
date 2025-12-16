using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Smaple01.Controllers
{
    [Route("api/v{version:apiVersion}/files")]
    //[Authorize]
    [ApiController]
    public class FileContoroller : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public FileContoroller(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }
        
        [HttpGet("{fileId}")]
        [ApiVersion(0.1, Deprecated = true)] 
        public ActionResult GetFile(int fileId)
        {
            string filePath = "Sample.pdf";

            if(!System.IO.File.Exists(filePath)) 
                return NotFound();

            if(!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        [HttpPost]
        public async Task<ActionResult> CreateFile(IFormFile file)
        {
            if (file.Length == 0 || file.Length > 20971520 || file.ContentType != "application/pdf")
                return BadRequest("INVALID");

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"uploadedfile_{Guid.NewGuid()}.pdf");

            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File has been uploaded succesfully");
        }
    }
}
