using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Models.Groups;
using StudentHub.Core.Entities.Groups;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Services.Groups
{
    public class GroupService : IGroupService
    {
        private readonly StudentHubDbContext _db;
        public GroupService(StudentHubDbContext db) => _db = db;

        public async Task<IEnumerable<GroupAdminDto>> GetAllAsync()
        {
            return await _db.Groups
                .Select(g => new GroupAdminDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Students = g.GroupStudents.Select(gs => new UserShortDto
                    {
                        Id = gs.Student.Id,
                        FirstName = gs.Student.FirstName,
                        LastName = gs.Student.LastName,
                        Email = gs.Student.Email!
                    }).ToList(),
                    Teachers = g.TeacherGroups.Select(tg => new UserShortDto
                    {
                        Id = tg.Teacher.Id,
                        FirstName = tg.Teacher.FirstName,
                        LastName = tg.Teacher.LastName,
                        Email = tg.Teacher.Email!
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Group> CreateAsync(CreateGroupDto dto)
        {
            var group = new Group { Name = dto.Name };
            _db.Groups.Add(group);
            await _db.SaveChangesAsync();
            return group;
        }

        public async Task AssignStudentsAsync(AssignStudentsDto dto)
        {
            foreach (var studentId in dto.StudentIds)
            {
                if (!await _db.GroupStudents.AnyAsync(x =>
                    x.GroupId == dto.GroupId && x.StudentId == studentId))
                {
                    _db.GroupStudents.Add(new GroupStudent
                    {
                        GroupId = dto.GroupId,
                        StudentId = studentId
                    });
                }
            }
            await _db.SaveChangesAsync();
        }

        public async Task AssignTeacherAsync(AssignTeacherToGroupDto dto)
        {
            var exists = await _db.TeacherGroups.AnyAsync(x =>
                x.GroupId == dto.GroupId &&
                x.TeacherId == dto.TeacherId);

            if (!exists)
            {
                _db.TeacherGroups.Add(new TeacherGroup
                {
                    GroupId = dto.GroupId,
                    TeacherId = dto.TeacherId
                });
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Group?> UpdateAsync(int id, UpdateGroupDto dto)
        {
            var group = await _db.Groups.FindAsync(id);
            if (group == null) return null;
            group.Name = dto.Name;
            await _db.SaveChangesAsync();
            return group;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _db.Groups.FindAsync(id);
            if (group == null) return false;
            _db.Groups.Remove(group);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
