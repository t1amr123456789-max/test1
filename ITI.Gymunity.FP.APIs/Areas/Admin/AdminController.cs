using ITI.Gymunity.FP.Application.DTOs.Admin;
using ITI.Gymunity.FP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Gymunity.FP.APIs.Areas.Admin
{
 [Area("Admin")]
 [Route("api/admin/[controller]")]
 [ApiController]
 public class AdminController : ControllerBase
 {
 private readonly IAdminService _service;

 public AdminController(IAdminService service)
 {
 _service = service;
 }

 [HttpGet("programs/pending")]
 public async Task<IActionResult> GetAllProgramsPending()
 {
 var list = await _service.GetAllPendingProgramsAsync();
 return Ok(list);
 }

 [HttpPut("programs/{programId:int}/approve")]
 public async Task<IActionResult> ApproveProgram(int programId, [FromBody] ProgramAdminStatusUpdateRequest request)
 {
 var ok = await _service.ApproveProgramAsync(programId, request.AdminUserId);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpPut("programs/{programId:int}/reject")]
 public async Task<IActionResult> RejectProgram(int programId, [FromBody] ProgramAdminStatusUpdateRequest request)
 {
 var ok = await _service.RejectProgramAsync(programId, request.AdminUserId, request.Reason);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpDelete("programs/{programId:int}")]
 public async Task<IActionResult> DeleteProgram(int programId)
 {
 var ok = await _service.DeleteProgramAsync(programId);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpGet("exercise-library/pending")]
 public async Task<IActionResult> GetAllExercisesPending()
 {
 var list = await _service.GetAllPendingExercisesAsync();
 return Ok(list);
 }

 [HttpPut("exercise-library/{exerciseId:int}/approve")]
 public async Task<IActionResult> ApproveExercise(int exerciseId, [FromBody] ExerciseAdminStatusUpdateRequest request)
 {
 var ok = await _service.ApproveExerciseAsync(exerciseId, request.AdminUserId);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpPut("exercise-library/{exerciseId:int}/reject")]
 public async Task<IActionResult> RejectExercise(int exerciseId, [FromBody] ExerciseAdminStatusUpdateRequest request)
 {
 var ok = await _service.RejectExerciseAsync(exerciseId, request.AdminUserId, request.Reason);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpDelete("exercise-library/{exerciseId:int}")]
 public async Task<IActionResult> DeleteExercise(int exerciseId)
 {
 var ok = await _service.DeleteExerciseAsync(exerciseId);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpGet("trainers/pending")]
 public async Task<IActionResult> GetAllPendingTrainers()
 {
 var list = await _service.GetAllPendingTrainersAsync();
 return Ok(list);
 }

 [HttpPut("trainers/{trainerId:int}/verify")]
 public async Task<IActionResult> VerifyTrainer(int trainerId, [FromBody] TrainerVerificationUpdateRequest request)
 {
 var ok = await _service.VerifyTrainerAsync(trainerId, request.AdminUserId);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpPut("trainers/{trainerId:int}/reject")]
 public async Task<IActionResult> RejectTrainer(int trainerId, [FromBody] TrainerVerificationUpdateRequest request)
 {
 var ok = await _service.RejectTrainerAsync(trainerId, request.AdminUserId, request.Reason);
 if (!ok) return NotFound();
 return NoContent();
 }

 [HttpDelete("trainers/{trainerId:int}")]
 public async Task<IActionResult> DeleteTrainer(int trainerId)
 {
 var ok = await _service.DeleteTrainerAccountAsync(trainerId);
 if (!ok) return NotFound();
 return NoContent();
 }
 }
}
