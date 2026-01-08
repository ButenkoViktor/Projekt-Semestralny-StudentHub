using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Models.Courses;
using StudentHub.Api.Models.Groups;
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

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.CourseGroups)
                    .ThenInclude(cg => cg.Group)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    TeacherId = c.TeacherId,
                    Groups = c.CourseGroups.Select(cg => new GroupShortDto
                    {
                        Id = cg.Group.Id,
                        Name = cg.Group.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
            => await _context.Courses
                .Include(c => c.CourseGroups)
                    .ThenInclude(cg => cg.Group)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Course>> GetByTeacherIdAsync(string teacherId)
            => await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();

        public async Task<IEnumerable<Course>> GetByStudentIdAsync(string studentId)
            => await _context.CourseGroups
                .Where(cg =>
                    cg.Group.GroupStudents.Any(gs => gs.StudentId == studentId))
                .Select(cg => cg.Course)
                .Distinct()
                .ToListAsync();

        public async Task<bool> TeacherHasAccessAsync(string teacherId, int courseId)
            => await _context.Courses
                .AnyAsync(c => c.Id == courseId && c.TeacherId == teacherId);

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
            existing.TeacherId = updated.TeacherId;

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

        public async Task AssignGroupAsync(int courseId, int groupId)
        {
            var exists = await _context.CourseGroups.AnyAsync(x =>
                x.CourseId == courseId && x.GroupId == groupId);

            if (!exists)
            {
                _context.CourseGroups.Add(new CourseGroup
                {
                    CourseId = courseId,
                    GroupId = groupId
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CourseWithTeacherDto>> GetByStudentIdWithTeacherAsync(string studentId)
        {
            return await _context.CourseGroups
                .Where(cg =>
                    cg.Group.GroupStudents.Any(gs => gs.StudentId == studentId))
                .Select(cg => cg.Course)
                .Distinct()
                .Include(c => c.Teacher)
                .Select(c => new CourseWithTeacherDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    TeacherFirstName = c.Teacher.FirstName,
                    TeacherLastName = c.Teacher.LastName,
                    TeacherEmail = c.Teacher.Email
                })
                .ToListAsync();
        }


    }
}
