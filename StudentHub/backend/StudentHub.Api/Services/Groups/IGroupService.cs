using StudentHub.Api.Models.Groups;

public interface IGroupService
{
    Task<IEnumerable<Group>> GetAllAsync();
    Task<Group?> GetByIdAsync(int id);
    Task<Group> CreateAsync(CreateGroupDto dto);
    Task<Group?> UpdateAsync(int id, UpdateGroupDto dto);
    Task<bool> DeleteAsync(int id);
    Task AssignStudentsAsync(AssignStudentsDto dto);
    Task AssignTeacherAsync(AssignTeacherToGroupDto dto);
}