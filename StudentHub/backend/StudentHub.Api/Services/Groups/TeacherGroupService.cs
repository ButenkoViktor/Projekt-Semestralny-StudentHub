using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Models.Grades;
using StudentHub.Api.Models.Groups;
using StudentHub.Core.Entities.Grades;
using StudentHub.Core.Entities.Groups;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Services.Groups
{
    public class TeacherGroupService : ITeacherGroupService
    {
        private readonly StudentHubDbContext _context;

        public TeacherGroupService(StudentHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GroupForTeacherDto>> GetMyGroupsAsync(string teacherId)
        {
            return await _context.TeacherCourseGroups
                .Include(x => x.Group)
                .Include(x => x.Course)
                .Where(x => x.TeacherId == teacherId)
                .Select(x => new GroupForTeacherDto
                {
                    GroupId = x.GroupId,
                    GroupName = x.Group.Name,
                    CourseId = x.CourseId,         
                    CourseTitle = x.Course.Title
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentGradeDto>> GetGroupStudentsAsync(
            string teacherId, int groupId, int courseId)
        {
            var hasAccess = await _context.TeacherCourseGroups.AnyAsync(x =>
                x.TeacherId == teacherId &&
                x.GroupId == groupId &&
                x.CourseId == courseId);

            if (!hasAccess)
                throw new UnauthorizedAccessException();

            var students = await _context.Groups
                .Where(g => g.Id == groupId)
                .SelectMany(g => g.Students!)
                .Select(s => new StudentGradeDto
                {
                    StudentId = s.Id,
                    StudentName = s.FirstName + " " + s.LastName,
                    Grade = null,
                    IsPresent = false
                })
                .ToListAsync();

            return students;
        }

        public async Task SaveGradeAsync(string teacherId, SaveGradeDto dto)
        {
            var hasAccess = await _context.TeacherCourseGroups.AnyAsync(x =>
                x.TeacherId == teacherId &&
                x.GroupId == dto.GroupId &&
                x.CourseId == dto.CourseId);

            if (!hasAccess)
                throw new UnauthorizedAccessException();

            var grade = await _context.StudentGrades.FirstOrDefaultAsync(x =>
                x.StudentId == dto.StudentId &&
                x.CourseId == dto.CourseId &&
                x.GroupId == dto.GroupId &&
                x.Date.Date == dto.Date.Date);

            if (grade == null)
            {
                grade = new StudentGrade
                {
                    StudentId = dto.StudentId,
                    CourseId = dto.CourseId,
                    GroupId = dto.GroupId,
                    Date = dto.Date,
                    Grade = dto.Grade,
                    IsPresent = dto.IsPresent
                };
                _context.StudentGrades.Add(grade);
            }
            else
            {
                grade.Grade = dto.Grade;
                grade.IsPresent = dto.IsPresent;
            }

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<StudentFinalGradeDto>> GetFinalGradesAsync(string teacherId, int groupId, int courseId)
        {
            var hasAccess = await _context.TeacherCourseGroups.AnyAsync(x =>
                x.TeacherId == teacherId &&
                x.GroupId == groupId &&
                x.CourseId == courseId);

            if (!hasAccess)
                throw new UnauthorizedAccessException();

            var grades = await _context.StudentGrades
                .Where(g =>
                    g.GroupId == groupId &&
                    g.CourseId == courseId)
                .GroupBy(g => g.StudentId)
                .Select(g => new StudentFinalGradeDto
                {
                    StudentId = g.Key,
                    StudentName = g.First().Student.FirstName + " " + g.First().Student.LastName,

                    TotalLessons = g.Count(),
                    AttendedLessons = g.Count(x => x.IsPresent),

                    FinalGrade = g
                        .Where(x => x.IsPresent && x.Grade != null)
                        .Average(x => (double)x.Grade!)
                })
                .ToListAsync();

            return grades;
        }
        public async Task<IEnumerable<StudentGradeDto>> GetGradesHistoryAsync(string teacherId, int groupId, int courseId)
        {
            var hasAccess = await _context.TeacherCourseGroups.AnyAsync(x =>
                x.TeacherId == teacherId &&
                x.GroupId == groupId &&
                x.CourseId == courseId);

            if (!hasAccess)
                throw new UnauthorizedAccessException();

            return await _context.StudentGrades
                .Where(g =>
                    g.CourseId == courseId &&
                    g.GroupId == groupId)
                .Select(g => new StudentGradeDto
                {
                    StudentId = g.StudentId,
                    StudentName = g.Student.FirstName + " " + g.Student.LastName,
                    Date = g.Date,
                    IsPresent = g.IsPresent,
                    Grade = g.Grade
                })
                .ToListAsync();
        }
    }
}
