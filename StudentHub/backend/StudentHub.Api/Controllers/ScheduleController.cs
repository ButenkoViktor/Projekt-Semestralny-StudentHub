using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentHub.Api.Models.Schedule;
using StudentHub.Api.Services.Schedule;
using StudentHub.Core.Entities.Schedule;
using StudentHub.Infrastructure.Data;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        private readonly StudentHubDbContext _context;

        public ScheduleController(
            IScheduleService service,
            StudentHubDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();

            return Ok(items.Select(s => new ScheduleDto
            {
                Id = s.Id,
                CourseTitle = s.Course.Title,
                TeacherName = s.TeacherName,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                GroupId = s.GroupId,
                LessonType = s.LessonType
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDto>> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            return new ScheduleDto
            {
                Id = item.Id,
                CourseTitle = item.Course.Title,
                TeacherName = item.TeacherName,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                GroupId = item.GroupId,
                LessonType = item.LessonType
            };
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult> Create(CreateScheduleDto dto)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == dto.CourseId);

            if (course == null)
                return BadRequest("Course not found");

            var item = new ScheduleItem
            {
                CourseId = dto.CourseId,
                TeacherName = $"{course.Teacher.FirstName} {course.Teacher.LastName}",
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                GroupId = dto.GroupId,
                LessonType = dto.LessonType
            };

            var created = await _service.CreateAsync(item);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult> Update(int id, UpdateScheduleDto dto)
        {
            var updated = await _service.UpdateAsync(id, new ScheduleItem
            {
                Id = id,
                CourseId = dto.CourseId,
                TeacherName = dto.TeacherName,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                GroupId = dto.GroupId,
                LessonType = dto.LessonType
            });

            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
