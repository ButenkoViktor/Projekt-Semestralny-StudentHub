using StudentHub.Api.Models.Groups;
using StudentHub.Core.Entities.Groups;

public interface IGroupService
{
    Task<IEnumerable<GroupAdminDto>> GetAllAsync();
    Task<Group> CreateAsync(CreateGroupDto dto);
    Task<Group?> UpdateAsync(int id, UpdateGroupDto dto);
    Task<bool> DeleteAsync(int id);
    Task AssignStudentsAsync(AssignStudentsDto dto);
    Task AssignTeacherAsync(AssignTeacherToGroupDto dto);
}
