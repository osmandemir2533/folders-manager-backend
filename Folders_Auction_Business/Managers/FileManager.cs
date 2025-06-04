using Folders_Auction_Business.Services;
using Folders_Auction_Core.DTOs;
using Folders_Auction_Core.Entities;
using Folders_Auction_Data_Access.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Folders_Auction_Business.Managers
{
    public class FileManager : IFileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FileManager(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<FileEntity> UploadFileAsync(int userId, IFormFile file)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var physicalPath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileEntity = new FileEntity
            {
                FileName = file.FileName,
                FilePath = uniqueFileName,
                ContentType = file.ContentType,
                Size = file.Length,
                UploadDate = DateTime.UtcNow,
                UploadedByUserId = userId
            };

            _context.Files.Add(fileEntity);
            await _context.SaveChangesAsync();

            return fileEntity;
        }

        public async Task<bool> DeleteFileAsync(int fileId, int userId)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == fileId && f.UploadedByUserId == userId);
            if (file == null) return false;

            var physicalPath = Path.Combine(_env.WebRootPath, "uploads", file.FilePath);
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<FileDto>> GetFilesByUserAsync(int userId)
        {
            return await _context.Files
                .Where(f => f.UploadedByUserId == userId)
                .Select(f => new FileDto
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                    ContentType = f.ContentType,
                    Size = f.Size,
                    UploadDate = f.UploadDate
                }).ToListAsync();
        }

        public async Task<FileDto?> GetFileByIdAsync(int id, int userId)
        {
            var file = await _context.Files
                .FirstOrDefaultAsync(f => f.Id == id && f.UploadedByUserId == userId);

            if (file == null) return null;

            return new FileDto
            {
                Id = file.Id,
                FileName = file.FileName,
                FilePath = file.FilePath,
                ContentType = file.ContentType,
                Size = file.Size,
                UploadDate = file.UploadDate
            };
        }

        public async Task<List<FileDto>> GetAllFilesAsync()
        {
            return await _context.Files
                .Select(f => new FileDto
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    FilePath = f.FilePath,
                    ContentType = f.ContentType,
                    Size = f.Size,
                    UploadDate = f.UploadDate
                }).ToListAsync();
        }

        public async Task<FileDto?> GetFileByIdAsync(int id)
        {
            var file = await _context.Files
                .FirstOrDefaultAsync(f => f.Id == id);

            if (file == null) return null;

            return new FileDto
            {
                Id = file.Id,
                FileName = file.FileName,
                FilePath = file.FilePath,
                ContentType = file.ContentType,
                Size = file.Size,
                UploadDate = file.UploadDate
            };
        }
    }
}
