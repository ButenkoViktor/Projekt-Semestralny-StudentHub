using StudentHub.Core.Entities.Tasks;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);

    Task<TaskSubmission> SubmitAsync(TaskSubmission submission);
    Task<TaskSubmissionFile> AddSubmissionFileAsync(TaskSubmissionFile file);
}
