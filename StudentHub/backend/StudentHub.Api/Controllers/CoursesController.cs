using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Courses;
using StudentHub.Api.Models.Groups;
using StudentHub.Api.Services.Groups;
using StudentHub.Core.Entities.Groups;
using System.Security.Claims;

namespace StudentHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Admin"))
                return Ok(await _service.GetAllAsync());

            if (User.IsInRole("Teacher"))
                return Ok(await _service.GetByTeacherIdAsync(userId));

            return Forbid();
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyCourses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Teacher"))
                return Ok(await _service.GetByTeacherIdAsync(userId));

            if (User.IsInRole("Student"))
                return Ok(await _service.GetByStudentIdAsync(userId));

            return Forbid();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.IsInRole("Admin"))
                return Ok(await _service.GetByIdAsync(id));

            if (User.IsInRole("Teacher") &&
                await _service.TeacherHasAccessAsync(userId, id))
                return Ok(await _service.GetByIdAsync(id));

            return Forbid();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                TeacherId = dto.TeacherId
            };

            return Ok(await _service.CreateAsync(course));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok)
                return NotFound();

            return NoContent();
        }

        [HttpPost("assign-group")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignGroup(AssignGroupToCourseDto dto)
        {
            await _service.AssignGroupAsync(dto.CourseId, dto.GroupId);
            return Ok();
        }
    }
}