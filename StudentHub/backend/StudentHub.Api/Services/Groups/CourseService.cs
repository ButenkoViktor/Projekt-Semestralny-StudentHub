using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Services.Groups;
using StudentHub.Core.Entities.Groups;
using StudentHub.Infrastructure.Data;

namespace StudentHub.API.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly StudentHubDbContext _context;

        public CourseService(StudentHubDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }


        public async Task<IEnumerable<Course>> GetByTeacherIdAsync(string teacherId)
        {
            return await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetByStudentIdAsync(string studentId)
        {
            return await _context.TeacherCourseGroups
                .Where(tc =>
                    tc.Group.Students.Any(s => s.Id == studentId)
                )
                .Include(tc => tc.Course)
                .Select(tc => tc.Course)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> TeacherHasAccessAsync(string teacherId, int courseId)
        {
            return await _context.Courses
                .AnyAsync(c => c.Id == courseId && c.TeacherId == teacherId);
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Tasks)
                .Include(c => c.ScheduleItems)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> CreateAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course?> UpdateAsync(int id, Course updated)
        {
            var existing = await _context.Courses.FindAsync(id);
            if (existing == null) return null;

            existing.Title = updated.Title;
            existing.Description = updated.Description;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}