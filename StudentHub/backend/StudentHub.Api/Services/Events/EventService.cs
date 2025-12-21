using Microsoft.EntityFrameworkCore;
using StudentHub.Infrastructure.Data;
using StudentHub.Core.Entities.Events;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Services.Events
{
    public class EventService : IEventService
    {
        private readonly StudentHubDbContext _context;

        public EventService(StudentHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events
                .OrderBy(e => e.StartDate)
                .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> CreateAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return ev;
        }

        public async Task<Event?> UpdateAsync(int id, Event ev)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return null;

            existing.Title = ev.Title;
            existing.Description = ev.Description;
            existing.StartDate = ev.StartDate;
            existing.EndDate = ev.EndDate;
            existing.GroupId = ev.GroupId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
