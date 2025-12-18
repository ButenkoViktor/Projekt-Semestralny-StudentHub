using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Tasks;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Services.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly StudentHubDbContext _context;

        public TaskService(StudentHubDbContext context)
        {
              _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
             return await _context.Tasks
              .Include(t => t.Attachments)
              .Include(t => t.Submissions)
              .ThenInclude(s => s.Files)
              .ToListAsync();
        }

         public async Task<TaskItem?> GetByIdAsync(int id)
         {
             return await _context.Tasks
               .Include(t => t.Attachments)
               .Include(t => t.Submissions)
               .ThenInclude(s => s.Files)
               .FirstOrDefaultAsync(t => t.Id == id);
         }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
               _context.Tasks.Add(task);
               await _context.SaveChangesAsync();
               return task;
        }

        public async Task<TaskSubmission> SubmitAsync(TaskSubmission submission)
        {
               _context.TaskSubmissions.Add(submission);
               await _context.SaveChangesAsync();
               return submission;
        }

        public async Task<TaskSubmissionFile> AddSubmissionFileAsync(TaskSubmissionFile file)
        {
               _context.TaskSubmissionFiles.Add(file);
               await _context.SaveChangesAsync();
               return file;
        }
    }
}
