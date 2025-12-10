using StudentHub.Api.Models.Annoucements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentHub.Api.Services.Announcements
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAsync();
        Task<AnnouncementDto?> GetByIdAsync(int id);
        Task<AnnouncementDto> CreateAsync(CreateAnnouncementRequest request, string authorId);
        Task<bool> DeleteAsync(int id, string requesterId, bool isAdmin);
    }
}