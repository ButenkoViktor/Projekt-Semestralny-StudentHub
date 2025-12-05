using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Tasks;
using StudentHub.Api.Services;
using StudentHub.Application.Services.Tasks;
using StudentHub.Core.Entities.Tasks;
using System.Security.Claims;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _taskService;
        private readonly IFileService _fileService;

        public TasksController(ITasksService taskService, IFileService fileService)
        {
            _taskService = taskService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Deadline = dto.Deadline,
                CourseId = dto.CourseId.Value,
                GroupId = dto.GroupId
            };

            var created = await _taskService.CreateAsync(task);
            return Ok(created);
        }

        [Authorize]
        [HttpPost("{id:int}/submit")]
        public async Task<IActionResult> Submit(int id, SubmitTaskDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var submission = new TaskSubmission
            {
                TaskId = id,
                UserId = userId,
                AnswerText = dto.AnswerText,
                SubmittedAt = DateTime.UtcNow
            };

            await _taskService.SubmitAsync(submission);
            return Ok(submission.Id);
        }

        [Authorize]
        [HttpPost("{submissionId:int}/submit-file")]
        public async Task<IActionResult> SubmitFile(int submissionId, IFormFile file)
        {
            var fileUrl = await _fileService.SaveFileAsync(file, "uploads/task-submissions");

            var submissionFile = new TaskSubmissionFile
            {
                FilePath = fileUrl,
                FileName = file.FileName,
                SubmissionId = submissionId
            };

            var submission = await _taskService.SubmitAsyncFile(submissionFile);

            return Ok(submissionFile);
        }
    }
}