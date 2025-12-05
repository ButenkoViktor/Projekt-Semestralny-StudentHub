using StudentHub.Core.Entities.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentHub.Application.Services.Tasks
{
    public interface ITasksService
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);

        Task<TaskSubmission> SubmitAsync(TaskSubmission submission);
        Task<TaskSubmissionFile> SubmitAsyncFile(TaskSubmissionFile file);
    }
}