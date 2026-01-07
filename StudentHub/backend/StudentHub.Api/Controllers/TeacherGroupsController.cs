using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Grades;
using StudentHub.Api.Services.Groups;
using System.Security.Claims;

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

    private string UserId =>
        User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetMyGroups()
    {
        return Ok(await _service.GetMyGroupsAsync(UserId));
    }

    [HttpGet("{groupId}/students")]
    public async Task<IActionResult> GetStudents(int groupId, [FromQuery] int courseId)
    {
        return Ok(await _service.GetGroupStudentsAsync(UserId, groupId, courseId));
    }

    [HttpPost("grade")]
    public async Task<IActionResult> SaveGrade([FromBody] SaveGradeDto dto)
    {
        await _service.SaveGradeAsync(UserId, dto);
        return Ok();
    }

    [HttpGet("{groupId}/courses/{courseId}/grades-history")]
    public async Task<IActionResult> GradesHistory(int groupId, int courseId)
    {
        return Ok(await _service.GetGradesHistoryAsync(UserId, groupId, courseId));
    }

    [HttpDelete("{groupId}/courses/{courseId}/grades")]
    public async Task<IActionResult> ClearAllGrades(int groupId, int courseId)
    {
        await _service.ClearGradesAsync(UserId, groupId, courseId);
        return Ok();
    }

    [HttpDelete("{groupId}/courses/{courseId}/grades/by-date")]
    public async Task<IActionResult> ClearGradesByDate(
        int groupId,
        int courseId,
        [FromQuery] DateTime date)
    {
        await _service.ClearGradesByDateAsync(UserId, groupId, courseId, date);
        return Ok();
    }
}
