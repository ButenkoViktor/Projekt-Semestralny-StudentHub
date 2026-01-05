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

        public async Task<IEnumerable<Group>> GetAllAsync() =>
            await _db.Groups
                .Include(g => g.GroupStudents)
                    .ThenInclude(gs => gs.Student)
                .Include(g => g.TeacherGroups)
                    .ThenInclude(tg => tg.Teacher)
                .ToListAsync();

        public async Task<Group?> GetByIdAsync(int id) =>
            await _db.Groups
                .Include(g => g.GroupStudents)
                    .ThenInclude(gs => gs.Student)
                .Include(g => g.TeacherGroups)
                    .ThenInclude(tg => tg.Teacher)
                .FirstOrDefaultAsync(g => g.Id == id);

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
            var exists = await _db.Set<TeacherGroup>().AnyAsync(x =>
                x.GroupId == dto.GroupId &&
                x.TeacherId == dto.TeacherId);

            if (!exists)
            {
                _db.Set<TeacherGroup>().Add(new TeacherGroup
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
