using StudentHub.Core.Entities.Events;

namespace StudentHub.Api.Services.Events
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task<Event> CreateAsync(Event ev);
        Task<Event?> UpdateAsync(int id, Event ev);
        Task<bool> DeleteAsync(int id);
    }
}
