using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Tasks;
using StudentHub.Api.Services.Tasks;
using StudentHub.Core.Entities.Tasks;
using System.Security.Claims;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [Authorize(Roles = "Teacher")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Deadline = dto.Deadline,
            CourseId = dto.CourseId,
            GroupId = dto.GroupId,
            IsPublished = true
        };

        var created = await _taskService.CreateAsync(task);
        return Ok(created);
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet("teacher")]
    public async Task<IActionResult> GetForTeacher()
    {
        var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return Ok(await _taskService.GetForTeacherAsync(teacherId));
    }

    [Authorize(Roles = "Student")]
    [HttpGet("student")]
    public async Task<IActionResult> GetForStudent()
    {
        var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return Ok(await _taskService.GetForStudentAsync(studentId));
    }

    [Authorize(Roles = "Student")]
    [HttpPost("{id:int}/submit")]
    public async Task<IActionResult> Submit(int id, SubmitTaskDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var task = await _taskService.GetByIdAsync(id);
        if (task == null) return NotFound();

        if (task.Submissions?.Any(s => s.UserId == userId) == true)
            return BadRequest("Task already submitted");

        var submission = new TaskSubmission
        {
            TaskId = id,
            Task = task,
            UserId = userId,
            AnswerText = dto.AnswerText,
            SubmittedAt = DateTime.UtcNow
        };

        return Ok(await _taskService.SubmitAsync(submission));
    }

    [Authorize(Roles = "Teacher")]
    [HttpGet("{id:int}/submissions")]
    public async Task<IActionResult> GetSubmissions(int id)
    {
        return Ok(await _taskService.GetSubmissionsForTaskAsync(id));
    }

    [Authorize(Roles = "Teacher")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteAsync(id);
        return NoContent();
    }
}
