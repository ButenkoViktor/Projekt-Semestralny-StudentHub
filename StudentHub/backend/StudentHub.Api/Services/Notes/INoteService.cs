using StudentHub.Core.Entities.Notes;

namespace StudentHub.Api.Services.Notes
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetForUserAsync(string userId);
        Task<Note?> GetByIdAsync(int id);
        Task<Note> CreateAsync(Note note);
        Task<Note?> UpdateAsync(Note note);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
