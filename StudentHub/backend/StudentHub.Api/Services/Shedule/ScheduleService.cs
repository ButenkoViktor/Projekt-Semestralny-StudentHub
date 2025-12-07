using System;
using Microsoft.EntityFrameworkCore;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Infrastructure.Data;
using StudentHub.Api.Models.Shedule;
namespace StudentHub.Api.Services.Shedule
{
    public class ScheduleService : IScheduleService
    {
        private readonly StudentHubDbContext _db;

        public ScheduleService(StudentHubDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ScheduleDto>> GetGroupSchedule(int groupId)
        {
            return await _db.ScheduleItems
                .Where(s => s.GroupId == groupId)
                .Include(s => s.Course)
                .Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    CourseTitle = s.Course.Title,
                    TeacherName = s.TeacherName,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    GroupId = s.GroupId,
                    LessonType = s.LessonType
                })
                .ToListAsync();
        }

        public async Task<ScheduleDto?> Create(CreateScheduleDto dto)
        {
            var entity = new ScheduleItem
            {
                CourseId = dto.CourseId,
                TeacherName = dto.TeacherName,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                GroupId = dto.GroupId,
                LessonType = dto.LessonType
            };

            _db.ScheduleItems.Add(entity);
            await _db.SaveChangesAsync();

            return new ScheduleDto
            {
                Id = entity.Id,
                CourseTitle = (await _db.Courses.FindAsync(dto.CourseId))?.Title?? "",
                TeacherName = entity.TeacherName,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                GroupId = entity.GroupId,
                LessonType = entity.LessonType
            };
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _db.ScheduleItems.FindAsync(id);
            if (entity == null) return false;

            _db.ScheduleItems.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
