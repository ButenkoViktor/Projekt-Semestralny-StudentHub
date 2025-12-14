using StudentHub.Core.Entities.Groups;

namespace StudentHub.Api.Services.Groups
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<Group?> GetByIdAsync(int id);
        Task<Group> CreateAsync(Group group);
        Task<Group?> UpdateAsync(int id, Group updated);
        Task<bool> DeleteAsync(int id);
    }
}