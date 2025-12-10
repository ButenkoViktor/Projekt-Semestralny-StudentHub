using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Annoucements;
using StudentHub.Api.Services.Announcements;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _service;
        public AnnouncementsController(IAnnouncementService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var a = await _service.GetByIdAsync(id);
            if (a == null) return NotFound();
            return Ok(a);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var created = await _service.CreateAsync(request, userId);
            return Ok(created);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isAdmin = User.IsInRole("Admin");
            return await _service.DeleteAsync(id, userId, isAdmin) ? Ok() : NotFound();
        }
    }
}