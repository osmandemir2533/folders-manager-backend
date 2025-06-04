using Folders_Auction_Business.Services;
using Folders_Auction_Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;

namespace Folders_Auction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var file = await _fileService.UploadFileAsync(userId, dto.File);
            return Ok(new
            {
                file.Id,
                file.FileName,
                file.ContentType,
                file.Size,
                file.UploadDate,
                file.FilePath
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserFiles()
        {
            var files = await _fileService.GetAllFilesAsync();
            return Ok(files);
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyFiles()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var files = await _fileService.GetFilesByUserAsync(userId);
            return Ok(files);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var file = await _fileService.GetFileByIdAsync(id, userId);

            if (file == null) return NotFound();

            return Ok(file);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _fileService.GetFileByIdAsync(id);

            if (file == null) return NotFound();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FilePath);
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, file.ContentType, file.FileName);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _fileService.DeleteFileAsync(id, userId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
