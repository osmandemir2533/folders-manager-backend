using Microsoft.AspNetCore.Http;

namespace Folders_Auction_Core.DTOs
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; } = null!;
    }
}
