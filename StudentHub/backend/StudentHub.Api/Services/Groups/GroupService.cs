using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Services.Groups;
using StudentHub.Core.Entities.Groups;
using StudentHub.Infrastructure.Data;

namespace StudentHub.API.Services.Groups
{
    public class GroupService : IGroupService
    {
        private readonly StudentHubDbContext _context;

        public GroupService(StudentHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups
                .Include(g => g.Courses)
                .Include(g => g.Students)
                .ToListAsync();
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _context.Groups
                .Include(g => g.Courses)
                .Include(g => g.Students)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Group> CreateAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group?> UpdateAsync(int id, Group updated)
        {
            var existing = await _context.Groups.FindAsync(id);
            if (existing == null) return null;

            existing.Name = updated.Name;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return false;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}