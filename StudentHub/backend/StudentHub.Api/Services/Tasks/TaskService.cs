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

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            // ✅ перевірка існування курсу
            var courseExists = await _context.Courses
                .AnyAsync(c => c.Id == task.CourseId);

            if (!courseExists)
            {
                throw new ArgumentException(
                    $"Course with id {task.CourseId} does not exist");
            }

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<IEnumerable<TaskItem>> GetForTeacherAsync(string teacherId)
        {
            return await _context.Tasks
                .Where(t =>
                    _context.TeacherCourseGroups.Any(tc =>
                        tc.TeacherId == teacherId &&
                        tc.CourseId == t.CourseId))
                .Include(t => t.Submissions)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetForStudentAsync(string studentId)
        {
            return await _context.Tasks
                .Include(t => t.Submissions!.Where(s => s.UserId == studentId))
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Submissions)
                .ThenInclude(s => s.Files)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
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

        public async Task<IEnumerable<TaskSubmission>> GetSubmissionsForTaskAsync(int taskId)
        {
            return await _context.TaskSubmissions
                .Where(s => s.TaskId == taskId)
                .Include(s => s.User)
                .Include(s => s.Files)
                .ToListAsync();
        }
    }
}