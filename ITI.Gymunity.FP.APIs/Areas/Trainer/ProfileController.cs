using ITI.Gymunity.FP.Application.DTOs.Trainer;
using ITI.Gymunity.FP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Gymunity.FP.APIs.Areas.Trainer
{
 [Area("Trainer")]
 [Route("api/trainer/profile")]
 [ApiController]
 public class ProfileController : TrainerBaseController
 {
 private readonly ITrainerProfileManagerService _service;

 public ProfileController(ITrainerProfileManagerService service)
 {
 _service = service;
 }

 [HttpGet("{trainerId:int}")]
 public async Task<IActionResult> GetTrainerProfileById(int trainerId)
 {
 var profile = await _service.GetByIdAsync(trainerId);
 if (profile == null) return NotFound();
 return Ok(profile);
 }

 [HttpPut("{trainerId:int}")]
 public async Task<IActionResult> UpdateTrainerProfile(int trainerId, [FromBody] TrainerProfileUpdateRequest request)
 {
 var ok = await _service.UpdateAsync(trainerId, request);
 if (!ok) return NotFound();
 return NoContent();
 }
 }
}
