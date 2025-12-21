using Microsoft.EntityFrameworkCore;
using StudentHub.Infrastructure.Data;
using StudentHub.Core.Entities.Notes;

namespace StudentHub.Api.Services.Notes
{
    public class NoteService : INoteService
    {
        private readonly StudentHubDbContext _context;

        public NoteService(StudentHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetForUserAsync(string userId)
        {
            return await _context.Notes
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task<Note> CreateAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Note?> UpdateAsync(Note note)
        {
            var existing = await _context.Notes.FindAsync(note.Id);
            if (existing == null) return null;

            existing.Title = note.Title;
            existing.Content = note.Content;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (note == null) return false;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
