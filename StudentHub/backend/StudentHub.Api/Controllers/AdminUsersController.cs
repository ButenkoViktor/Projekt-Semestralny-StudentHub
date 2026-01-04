using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentHub.Api.Models.Admin;
using StudentHub.Core.Entities.Identity;

namespace StudentHub.Api.Controllers
{
    [ApiController]
    [Route("api/admin/users")]
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUsersController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var result = new List<AdminUserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                result.Add(new AdminUserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList()
                });
            }

            return Ok(result);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                return BadRequest("Role does not exist");

            if (await _userManager.IsInRoleAsync(user, dto.Role))
                return BadRequest("User already has this role");

            var result = await _userManager.AddToRoleAsync(user, dto.Role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                return BadRequest("Role does not exist");

            if (!await _userManager.IsInRoleAsync(user, dto.Role))
                return BadRequest("User does not have this role");

            var result = await _userManager.RemoveFromRoleAsync(user, dto.Role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateUserEmail([FromBody] UpdateUserEmailDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found");

            var emailExists = await _userManager.FindByEmailAsync(dto.NewEmail);
            if (emailExists != null && emailExists.Id != user.Id)
                return BadRequest("Email already in use");

            user.Email = dto.NewEmail;
            user.UserName = dto.NewEmail;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}
