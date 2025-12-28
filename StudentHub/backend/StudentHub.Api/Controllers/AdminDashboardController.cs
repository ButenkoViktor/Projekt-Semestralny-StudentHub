using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Entities.Identity;
using StudentHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/admin/dashboard")]
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly StudentHubDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminDashboardController(
            StudentHubDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var usersCount = await _userManager.Users.CountAsync();
            var coursesCount = await _context.Courses.CountAsync();
            var groupsCount = await _context.Groups.CountAsync();

            return Ok(new
            {
                users = usersCount,
                courses = coursesCount,
                groups = groupsCount
            });
        }
    }
}
