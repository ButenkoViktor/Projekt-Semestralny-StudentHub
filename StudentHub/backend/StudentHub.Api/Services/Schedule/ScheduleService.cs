using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Services.Schedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly StudentHubDbContext _context;

        public ScheduleService(StudentHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ScheduleItem>> GetAllAsync()
        {
            return await _context.ScheduleItems
                .Include(s => s.Course)
                .Include(s => s.Group)
                .ToListAsync();
        }

        public async Task<ScheduleItem?> GetByIdAsync(int id)
        {
            return await _context.ScheduleItems
                .Include(s => s.Course)
                .Include(s => s.Group)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ScheduleItem> CreateAsync(ScheduleItem item)
        {
            _context.ScheduleItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ScheduleItem?> UpdateAsync(int id, ScheduleItem item)
        {
            var existing = await _context.ScheduleItems.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.ScheduleItems.FindAsync(id);
            if (item == null) return false;

            _context.ScheduleItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
