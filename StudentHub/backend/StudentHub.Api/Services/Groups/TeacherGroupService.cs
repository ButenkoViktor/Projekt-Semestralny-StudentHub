using StudentHub.Api.Models.Grades;
using StudentHub.Api.Models.Groups;
using StudentHub.Api.Services.Groups;
using StudentHub.Core.Entities.Grades;
using StudentHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
public class TeacherGroupService : ITeacherGroupService
{
    private readonly StudentHubDbContext _context;

    public TeacherGroupService(StudentHubDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GroupForTeacherDto>> GetMyGroupsAsync(string teacherId)
    {
        return await _context.TeacherGroups
            .Where(tg => tg.TeacherId == teacherId)
            .SelectMany(tg => tg.Group.CourseGroups.Select(cg =>
                new GroupForTeacherDto
                {
                    GroupId = tg.GroupId,
                    GroupName = tg.Group.Name,
                    CourseId = cg.CourseId,
                    CourseTitle = cg.Course.Title
                }))
            .ToListAsync();
    }

    public async Task<IEnumerable<StudentGradeDto>> GetGroupStudentsAsync(
        string teacherId, int groupId, int courseId)
    {
        var hasAccess = await _context.TeacherGroups.AnyAsync(x =>
            x.TeacherId == teacherId && x.GroupId == groupId);

        if (!hasAccess)
            throw new UnauthorizedAccessException();

        return await _context.GroupStudents
            .Where(gs => gs.GroupId == groupId)
            .Select(gs => new StudentGradeDto
            {
                StudentId = gs.StudentId,
                StudentName = gs.Student.FirstName + " " + gs.Student.LastName,
                IsPresent = false,
                Grade = null
            })
            .ToListAsync();
    }

    public async Task SaveGradeAsync(string teacherId, SaveGradeDto dto)
    {
        var hasAccess = await _context.TeacherGroups.AnyAsync(x =>
            x.TeacherId == teacherId && x.GroupId == dto.GroupId);

        if (!hasAccess)
            throw new UnauthorizedAccessException();

        var grade = await _context.StudentGrades.FirstOrDefaultAsync(x =>
            x.StudentId == dto.StudentId &&
            x.GroupId == dto.GroupId &&
            x.CourseId == dto.CourseId &&
            x.Date.Date == dto.Date.Date);

        if (grade == null)
        {
            grade = new StudentGrade
            {
                StudentId = dto.StudentId,
                GroupId = dto.GroupId,
                CourseId = dto.CourseId,
                Date = dto.Date,
                IsPresent = dto.IsPresent,
                Grade = dto.Grade
            };
            _context.StudentGrades.Add(grade);
        }
        else
        {
            grade.IsPresent = dto.IsPresent;
            grade.Grade = dto.Grade;
        }

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<StudentGradeDto>> GetGradesHistoryAsync(
        string teacherId, int groupId, int courseId)
    {
        return await _context.StudentGrades
            .Where(x => x.GroupId == groupId && x.CourseId == courseId)
            .Select(x => new StudentGradeDto
            {
                StudentId = x.StudentId,
                StudentName = x.Student.FirstName + " " + x.Student.LastName,
                Date = x.Date,
                IsPresent = x.IsPresent,
                Grade = x.Grade
            })
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }
   
    public async Task ClearGradesAsync(string teacherId, int groupId, int courseId)
    {
        var hasAccess = await _context.TeacherGroups.AnyAsync(x =>
            x.TeacherId == teacherId && x.GroupId == groupId);

        if (!hasAccess)
            throw new UnauthorizedAccessException();

        var grades = await _context.StudentGrades
            .Where(x => x.GroupId == groupId && x.CourseId == courseId)
            .ToListAsync();

        _context.StudentGrades.RemoveRange(grades);
        await _context.SaveChangesAsync();
    }

    public async Task ClearGradesByDateAsync(
        string teacherId,
        int groupId,
        int courseId,
        DateTime date)
    {
        var hasAccess = await _context.TeacherGroups.AnyAsync(x =>
            x.TeacherId == teacherId && x.GroupId == groupId);

        if (!hasAccess)
            throw new UnauthorizedAccessException();

        var grades = await _context.StudentGrades
            .Where(x =>
                x.GroupId == groupId &&
                x.CourseId == courseId &&
                x.Date.Date == date.Date)
            .ToListAsync();

        _context.StudentGrades.RemoveRange(grades);
        await _context.SaveChangesAsync();
    }
}
