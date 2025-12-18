using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Tasks;
using StudentHub.Api.Services.Files;
using StudentHub.Api.Services.Tasks;
using StudentHub.Core.Entities.Tasks;
using System.Security.Claims;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _tasksService;
        private readonly IFileService _fileService;

        public TasksController(
            ITaskService tasksService,
            IFileService fileService)
        {
            _tasksService = tasksService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tasksService.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _tasksService.GetByIdAsync(id);
            return task == null ? NotFound() : Ok(task);
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
                CourseId = dto.CourseId,
                GroupId = dto.GroupId
            };

            var created = await _tasksService.CreateAsync(task);
            return Ok(created);
        }

        [Authorize]
        [HttpPost("{id:int}/submit")]
        public async Task<IActionResult> Submit(int id, SubmitTaskDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var submission = new TaskSubmission
            {
                TaskId = id,
                UserId = userId!,
                AnswerText = dto.AnswerText,
                SubmittedAt = DateTime.UtcNow
            };

            var created = await _tasksService.SubmitAsync(submission);
            return Ok(created);
        }

        [Authorize]
        [HttpPost("submission/{submissionId:int}/file")]
        public async Task<IActionResult> UploadSubmissionFile(
            int submissionId,
            IFormFile file)
        {
            var record = await _fileService.UploadAsync(
                file,
                User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

            var submissionFile = new TaskSubmissionFile
            {
                SubmissionId = submissionId,
                FileName = record.FileName,
                FilePath = record.FilePath
            };

            var created = await _tasksService.AddSubmissionFileAsync(submissionFile);
            return Ok(created);
        }
    }
}