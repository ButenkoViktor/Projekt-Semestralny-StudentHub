using StudentHub.Api.Models.Groups;
using StudentHub.Core.Entities.Groups;

namespace StudentHub.Api.Services.Groups
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<Group?> GetByIdAsync(int id);
        Task<Group> CreateAsync(CreateGroupDto dto);
        Task<Group?> UpdateAsync(int id, UpdateGroupDto dto);
        Task<bool> DeleteAsync(int id);
        Task AssignStudentsAsync(AssignStudentsDto dto);
    }
}