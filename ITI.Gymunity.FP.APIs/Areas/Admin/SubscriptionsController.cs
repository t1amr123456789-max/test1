using ITI.Gymunity.FP.Application.DTOs.Admin;
using ITI.Gymunity.FP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Gymunity.FP.APIs.Areas.Admin
{
 [Area("Admin")]
 [Route("api/admin/[controller]")]
 [ApiController]
 public class SubscriptionsController : ControllerBase
 {
 private readonly ISubscriptionAdminService _service;

 public SubscriptionsController(ISubscriptionAdminService service)
 {
 _service = service;
 }

 [HttpGet]
 public async Task<IActionResult> GetAll()
 {
 var list = await _service.GetAllSubscriptionsAsync();
 return Ok(list);
 }

 [HttpPut("{subscriptionId:int}/status")]
 public async Task<IActionResult> UpdateStatus(int subscriptionId, [FromBody] SubscriptionStatusUpdateRequest request)
 {
 var ok = await _service.UpdateSubscriptionStatusAsync(subscriptionId, request.NewStatus);
 if (!ok) return NotFound();
 return NoContent();
 }
 }
}
