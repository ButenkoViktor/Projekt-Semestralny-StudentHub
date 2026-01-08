using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Models.Tasks;
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

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == task.CourseId);

            if (!courseExists)
                throw new ArgumentException("Course not found");

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<TaskItem>> GetForTeacherAsync(string teacherId)
        {
            return await _context.Tasks
                .Where(t =>
                    _context.Courses.Any(c =>
                        c.Id == t.CourseId &&
                        c.TeacherId == teacherId))
                .OrderBy(t => t.Deadline)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetForStudentAsync(string studentId)
        {
            return await _context.Tasks
                .Include(t => t.Submissions!
                    .Where(s => s.UserId == studentId))
                .OrderBy(t => t.Deadline)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Submissions)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskSubmission> SubmitAsync(TaskSubmission submission)
        {
            submission.Status =
                submission.SubmittedAt > submission.Task.Deadline
                    ? TaskSubmissionStatus.Late
                    : TaskSubmissionStatus.Submitted;

            _context.TaskSubmissions.Add(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        public async Task<TaskSubmission> UpdateSubmissionAsync(
    int taskId,
    string userId,
    SubmitTaskDto dto)
        {
            var submission = await _context.TaskSubmissions
                .Include(s => s.Task)
                .FirstOrDefaultAsync(s =>
                    s.TaskId == taskId &&
                    s.UserId == userId);

            if (submission == null)
                throw new Exception("Submission not found");

            if (DateTime.UtcNow > submission.Task.Deadline)
                throw new Exception("Deadline passed");

            submission.AnswerText = dto.AnswerText;
            submission.SubmittedAt = DateTime.UtcNow;
            submission.Status =
                submission.SubmittedAt > submission.Task.Deadline
                    ? TaskSubmissionStatus.Late
                    : TaskSubmissionStatus.Submitted;

            await _context.SaveChangesAsync();
            return submission;
        }

        public async Task<IEnumerable<TaskSubmission>> GetSubmissionsForTaskAsync(int taskId)
        {
            return await _context.TaskSubmissions
                .Where(s => s.TaskId == taskId)
                .Include(s => s.User)
                .ToListAsync();
        }
    }
}
