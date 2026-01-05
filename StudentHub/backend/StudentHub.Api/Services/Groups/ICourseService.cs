using StudentHub.Core.Entities.Groups;

namespace StudentHub.Api.Services.Groups
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();         
        Task<Course?> GetByIdAsync(int id);
        Task<Course> CreateAsync(Course course);
        Task<Course?> UpdateAsync(int id, Course updated);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Course>> GetByTeacherIdAsync(string teacherId);
        Task<IEnumerable<Course>> GetByStudentIdAsync(string studentId);
        Task<bool> TeacherHasAccessAsync(string teacherId, int courseId);
        Task AssignGroupAsync(int courseId, int groupId);
    }
}