using StudentHub.Api.Models.Shedule;
namespace StudentHub.Api.Services.Shedule
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetGroupSchedule(int groupId);
        Task<ScheduleDto?> Create(CreateScheduleDto dto);
        Task<bool> Delete(int id);
    }
}
