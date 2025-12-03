using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace StudentHub.Api.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folder);
    }
}
