using StudentHub.Core.Entities.Schedule;

namespace StudentHub.Api.Services.Schedule
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleItem>> GetAllAsync();
        Task<ScheduleItem?> GetByIdAsync(int id);
        Task<ScheduleItem> CreateAsync(ScheduleItem item);
        Task<ScheduleItem?> UpdateAsync(int id, ScheduleItem item);
        Task<bool> DeleteAsync(int id);
    }
}
