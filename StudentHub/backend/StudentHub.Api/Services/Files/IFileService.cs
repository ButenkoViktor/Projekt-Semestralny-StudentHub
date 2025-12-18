using Microsoft.AspNetCore.Http;
using StudentHub.Core.Entities.Files;

public interface IFileService
{
    Task<FileStorageRecord> UploadAsync(IFormFile file, string? userId);
    Task<FileStorageRecord?> GetByIdAsync(int id);
    Task<IEnumerable<FileStorageRecord>> GetAllAsync();
    Task DeleteAsync(int id);
}