using StudentHub.Api.Models.Grades;
using StudentHub.Api.Models.Groups;

namespace StudentHub.Api.Services.Groups
{
    public interface ITeacherGroupService
    {
        Task<IEnumerable<GroupForTeacherDto>> GetMyGroupsAsync(string teacherId);
        Task<IEnumerable<StudentGradeDto>> GetGroupStudentsAsync(string teacherId, int groupId, int courseId);
        Task<IEnumerable<StudentFinalGradeDto>> GetFinalGradesAsync(string teacherId, int groupId, int courseId);
        Task<IEnumerable<StudentGradeDto>> GetGradesHistoryAsync(string teacherId, int groupId, int courseId
);
        Task SaveGradeAsync(string teacherId, SaveGradeDto dto);
    }
}
