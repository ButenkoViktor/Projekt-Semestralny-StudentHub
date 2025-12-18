using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Files;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Services.Files
{
    public class FileService : IFileService
    {
        private readonly StudentHubDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FileService(StudentHubDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<FileStorageRecord> UploadAsync(IFormFile file, string? userId)
        {
            // Ensure upload directory exists
            var uploadsPath = Path.Combine(_env.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            // Generate unique file name
            var uniqueName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsPath, uniqueName);

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var record = new FileStorageRecord
            {
                FileName = file.FileName,
                FilePath = filePath,
                UploadedById = userId
            };

            _context.Files.Add(record);
            await _context.SaveChangesAsync();

            return record;
        }

        public async Task<FileStorageRecord?> GetByIdAsync(int id)
        {
            return await _context.Files
                .Include(f => f.UploadedBy)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<FileStorageRecord>> GetAllAsync()
        {
            return await _context.Files
                .OrderByDescending(f => f.UploadedAt)
                .ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);
            if (file == null)
                return;

            if (File.Exists(file.FilePath))
                File.Delete(file.FilePath);

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}