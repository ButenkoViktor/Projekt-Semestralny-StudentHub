using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Core.Entities.Identity;
using StudentHub.Api.Models.Roles;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return Ok(roles);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserWithRolesDto>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                result.Add(new UserWithRolesDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Roles = roles.ToList()
                });
            }

            return Ok(result);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                return BadRequest("Role does not exist");

            var already = await _userManager.IsInRoleAsync(user, dto.Role);
            if (already) return BadRequest("User already has this role");

            var res = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return Ok($"Role '{dto.Role}' assigned to user {user.Email}");
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                return BadRequest("Role does not exist");

            var inRole = await _userManager.IsInRoleAsync(user, dto.Role);
            if (!inRole) return BadRequest("User does not have this role");

            var res = await _userManager.RemoveFromRoleAsync(user, dto.Role);
            if (!res.Succeeded) return BadRequest(res.Errors);

            return Ok($"Role '{dto.Role}' removed from user {user.Email}");
        }
    }
}