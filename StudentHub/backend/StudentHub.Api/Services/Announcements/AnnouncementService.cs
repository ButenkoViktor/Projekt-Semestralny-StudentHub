using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Services.Announcements;
using StudentHub.Core.Entities.Announcements;
using StudentHub.Infrastructure.Data;
using StudentHub.Api.Models.Annoucements;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StudentHub.Infrastructure.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly StudentHubDbContext _db;
        public AnnouncementService(StudentHubDbContext db) => _db = db;

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
        {
            return await _db.Announcements
                .Include(a => a.Author)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AnnouncementDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    AuthorId = a.AuthorId,
                    AuthorName = a.Author.FirstName + " " + a.Author.LastName,
                    GroupId = a.GroupId,
                    Published = a.Published,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            var a = await _db.Announcements.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
            if (a == null) return null;
            return new AnnouncementDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                AuthorId = a.AuthorId,
                AuthorName = a.Author.FirstName + " " + a.Author.LastName,
                GroupId = a.GroupId,
                Published = a.Published,
                CreatedAt = a.CreatedAt
            };
        }

        public async Task<AnnouncementDto> CreateAsync(CreateAnnouncementRequest request, string authorId)
        {
            var entity = new Announcement
            {
                Title = request.Title,
                Content = request.Content,
                GroupId = request.GroupId,
                Published = request.Published,
                TargetGroup = request.TargetGroup,
                AuthorId = authorId,
                CreatedAt = DateTime.UtcNow
            };

            _db.Announcements.Add(entity);
            await _db.SaveChangesAsync();

            return await GetByIdAsync(entity.Id) ?? throw new Exception("Created but cannot read back entity.");
        }

        public async Task<bool> DeleteAsync(int id, string requesterId, bool isAdmin)
        {
            var entity = await _db.Announcements.FindAsync(id);
            if (entity == null) return false;
            if (!isAdmin && entity.AuthorId != requesterId) return false;
            _db.Announcements.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}