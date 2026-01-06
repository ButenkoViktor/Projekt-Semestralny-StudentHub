using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Groups;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/admin/courses")]
    [Authorize(Roles = "Admin")]
    public class AdminCoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public AdminCoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("assign-group")]
        public async Task<IActionResult> AssignGroupToCourse(
            [FromBody] AssignGroupToCourseDto dto)
        {
            await _courseService.AssignGroupAsync(dto.CourseId, dto.GroupId);
            return Ok();
        }
    }
}
