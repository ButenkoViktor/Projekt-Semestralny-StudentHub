using StudentHub.Api.Models.Courses;
using StudentHub.Core.Entities.Groups;

public interface ICourseService
{
    Task AssignGroupAsync(int courseId, int groupId);

    Task<IEnumerable<CourseDto>> GetAllAsync(); 

    Task<Course?> GetByIdAsync(int id);
    Task<IEnumerable<Course>> GetByTeacherIdAsync(string teacherId);
    Task<IEnumerable<Course>> GetByStudentIdAsync(string studentId);
    Task<bool> DeleteAsync(int id);
    Task<Course> CreateAsync(Course course);
    Task<bool> TeacherHasAccessAsync(string teacherId, int courseId);
}
