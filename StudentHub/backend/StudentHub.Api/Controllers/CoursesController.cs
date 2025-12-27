using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Courses;
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
            var isAdmin = User.IsInRole("Admin");
            var isTeacher = User.IsInRole("Teacher");

            if (isAdmin)
            {
               
                var courses = await _service.GetAllAsync();
                return Ok(courses);
            }

            if (isTeacher)
            {
              
                var courses = await _service.GetByTeacherIdAsync(userId);
                return Ok(courses);
            }

            return Forbid();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            var isTeacher = User.IsInRole("Teacher");

            var course = await _service.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            if (isAdmin)
                return Ok(course);

            if (isTeacher)
            {
                var hasAccess = await _service.TeacherHasAccessAsync(userId, id);
                if (!hasAccess)
                    return Forbid();

                return Ok(course);
            }

            return Forbid();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description
            };

            var created = await _service.CreateAsync(course);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateCourseDto dto)
        {
            var updated = new Course
            {
                Title = dto.Title,
                Description = dto.Description
            };

            var result = await _service.UpdateAsync(id, updated);
            if (result == null)
                return NotFound();

            return Ok(result);
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
    }
}