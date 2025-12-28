using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Grades;
using StudentHub.Api.Services.Groups;
using System.Security.Claims;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/teacher/groups")]
    [Authorize(Roles = "Teacher")]
    public class TeacherGroupsController : ControllerBase
    {
        private readonly ITeacherGroupService _service;

        public TeacherGroupsController(ITeacherGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> MyGroups()
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _service.GetMyGroupsAsync(teacherId));
        }

        [HttpGet("{groupId}/course/{courseId}")]
        public async Task<IActionResult> GroupStudents(int groupId, int courseId)
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _service.GetGroupStudentsAsync(teacherId, groupId, courseId));
        }

        [HttpPost("grade")]
        public async Task<IActionResult> SaveGrade(SaveGradeDto dto)
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.SaveGradeAsync(teacherId, dto);
            return Ok();
        }

        [HttpGet("{groupId}/courses/{courseId}/final-grades")]
        public async Task<IActionResult> GetFinalGrades(int groupId, int courseId)
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _service.GetFinalGradesAsync(
                teacherId!, groupId, courseId);

            return Ok(result);
        }

        [HttpGet("{groupId}/courses/{courseId}/grades-history")]
        public async Task<IActionResult> GetGradesHistory( int groupId, int courseId)
        {
            var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.GetGradesHistoryAsync(
                teacherId!, groupId, courseId);
            return Ok(result);
        }
    }
}
