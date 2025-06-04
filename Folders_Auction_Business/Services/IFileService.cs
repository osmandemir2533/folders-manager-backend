using Folders_Auction_Core.DTOs;
using Folders_Auction_Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Folders_Auction_Business.Services
{
    public interface IFileService
    {
        Task<FileEntity> UploadFileAsync(int userId, IFormFile file);
        Task<bool> DeleteFileAsync(int fileId, int userId);
        Task<List<FileDto>> GetFilesByUserAsync(int userId);
        Task<FileDto?> GetFileByIdAsync(int id, int userId);
        Task<List<FileDto>> GetAllFilesAsync();
        Task<FileDto?> GetFileByIdAsync(int id);
    }
}
