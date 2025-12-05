using ITI.Gymunity.FP.Application.DTOs.Admin;
using ITI.Gymunity.FP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Gymunity.FP.APIs.Areas.Admin
{
 [Area("Admin")]
 [Route("api/admin/[controller]")]
 [ApiController]
 public class UsersController : ControllerBase
 {
 private readonly IUserAdminService _service;

 public UsersController(IUserAdminService service)
 {
 _service = service;
 }

 [HttpGet]
 public async Task<IActionResult> GetAll()
 {
 var list = await _service.GetAllUsersAsync();
 return Ok(list);
 }

 [HttpGet("{userId}")]
 public async Task<IActionResult> GetById(string userId)
 {
 var item = await _service.GetByIdAsync(userId);
 if (item == null) return NotFound();
 return Ok(item);
 }

 [HttpPut("{userId}/role")]
 public async Task<IActionResult> UpdateRole(string userId, [FromBody] UserRoleUpdateRequest request)
 {
 var ok = await _service.UpdateUserRoleAsync(userId, request.NewRole);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpDelete("{userId}")]
 public async Task<IActionResult> Delete(string userId)
 {
 var ok = await _service.DeleteUserAsync(userId);
 if (!ok) return NotFound();
 return NoContent();
 }
 }
}
