using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Groups;
using StudentHub.Api.Services.Groups;
using System.Threading.Tasks;
namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/admin/groups")]
    [Authorize(Roles = "Admin")]
    public class AdminGroupsController : ControllerBase
    {
        private readonly IGroupService _service;
        public AdminGroupsController(IGroupService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupDto dto) =>
            Ok(await _service.CreateAsync(dto));

        [HttpPost("assign-students")]
        public async Task<IActionResult> AssignStudents(AssignStudentsDto dto)
        {
            await _service.AssignStudentsAsync(dto);
            return Ok();
        }

        [HttpPost("assign-teacher")]
        public async Task<IActionResult> AssignTeacher(AssignTeacherToGroupDto dto)
        {
            await _service.AssignTeacherAsync(dto);
            return Ok();
        }
    }
}