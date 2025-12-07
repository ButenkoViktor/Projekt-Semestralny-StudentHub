using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Shedule;
using StudentHub.Api.Services.Shedule;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }

        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetGroupSchedule(int groupId)
        {
            var data = await _service.GetGroupSchedule(groupId);
            return Ok(data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Create(CreateScheduleDto dto)
        {
            var item = await _service.Create(dto);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.Delete(id);
            return ok ? Ok() : NotFound();
        }
    }
}