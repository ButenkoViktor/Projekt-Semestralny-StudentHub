using StudentHub.Api.Models.Grades;
using StudentHub.Api.Models.Groups;

namespace StudentHub.Api.Services.Groups
{
    public interface ITeacherGroupService
    {
        Task<IEnumerable<GroupForTeacherDto>> GetMyGroupsAsync(string teacherId);
        Task<IEnumerable<StudentGradeDto>> GetGroupStudentsAsync(string teacherId, int groupId, int courseId);
        Task SaveGradeAsync(string teacherId, SaveGradeDto dto);
        Task<IEnumerable<StudentGradeDto>> GetGradesHistoryAsync(string teacherId, int groupId, int courseId);
        Task ClearGradesAsync(string teacherId, int groupId, int courseId);
        Task ClearGradesByDateAsync(string teacherId, int groupId, int courseId, DateTime date);
    }
}
