using StudentHub.Core.Entities.Tasks;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetForTeacherAsync(string teacherId);
    Task<IEnumerable<TaskItem>> GetForStudentAsync(string studentId);

    Task<TaskItem?> GetByIdAsync(int id);

    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem> UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);

    Task<TaskSubmission> SubmitAsync(TaskSubmission submission);

    Task<IEnumerable<TaskSubmission>> GetSubmissionsForTaskAsync(int taskId);
}