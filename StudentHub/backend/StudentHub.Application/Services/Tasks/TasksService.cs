using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Tasks;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Application.Services.Tasks
{
    public class TasksService : ITasksService
    {
        private readonly StudentHubDbContext _db;

        public TasksService(StudentHubDbContext db)
        {
            _db = db;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _db.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _db.Tasks
                .Include(t => t.Attachments)
                .Include(t => t.Submissions)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _db.Tasks.FindAsync(id);
            if (item == null) return false;

            _db.Tasks.Remove(item);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TaskSubmission> SubmitAsync(TaskSubmission submission)
        {
            _db.TaskSubmissions.Add(submission);
            await _db.SaveChangesAsync();
            return submission;
        }

        public async Task<TaskSubmissionFile> SubmitAsyncFile(TaskSubmissionFile file)
        {
            _db.TaskSubmissionFiles.Add(file);
            await _db.SaveChangesAsync();
            return file;
        }
    }
}
