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
    [HttpGet("teacher")]
    public async Task<IActionResult> GetForTeacher()
    {
        var teacherId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return Ok(await _taskService.GetForTeacherAsync(teacherId));
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
            GroupId = dto.GroupId
        };

        try
        {
            return Ok(await _taskService.CreateAsync(task));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Teacher")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTaskDto dto)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null) return NotFound();

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.Deadline = dto.Deadline;

        return Ok(await _taskService.UpdateAsync(task));
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteAsync(id);
        return NoContent();
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
        var task = await _taskService.GetByIdAsync(id);
        if (task == null) return NotFound();

        var submission = new TaskSubmission
        {
            TaskId = id,
            Task = task,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!,
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
}