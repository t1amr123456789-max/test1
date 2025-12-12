using ITI.Gymunity.FP.APIs.Errors;
using ITI.Gymunity.FP.Application.DTOs.Trainer;
using ITI.Gymunity.FP.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.Gymunity.FP.APIs.Areas.Trainer
{
    [Area("Trainer")]
    [Route("api/trainer/[controller]")]
    [ApiController]
    public class TrainerProfileController : TrainerBaseController
    {
        private readonly TrainerProfileService _trainerProfileService;
        private readonly ITrainerProfileManagerService _managerService;

        public TrainerProfileController(TrainerProfileService trainerProfileService, ITrainerProfileManagerService managerService)
        {
            _trainerProfileService = trainerProfileService;
            _managerService = managerService;
        }

        // GET: api/trainer/trainerprofile/allprofiles
        [HttpGet("AllProfiles")]
        [ProducesResponseType(typeof(IEnumerable<TrainerProfileResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _trainerProfileService.GetAllProfiles();

            if (!profiles.Any())
                return NotFound(new ApiResponse(404, "No trainer profiles found."));

            return Ok(profiles);
        }

        // GET: api/trainer/trainerprofile/userid/{userId}
        [HttpGet("UserId/{userId}")]
        [ProducesResponseType(typeof(TrainerProfileGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            // Not enough service surface to get by userId directly. Return NotFound for now.
            return NotFound(new ApiResponse(404, "Endpoint not implemented: GetByUserId."));
        }

        // GET: api/trainer/trainerprofile/id/{id}
        [HttpGet("Id/{id}")]
        [ProducesResponseType(typeof(TrainerProfileGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _managerService.GetByIdAsync(id);

            if (profile == null)
                return NotFound(new ApiResponse(404, "Trainer profile not found."));

            return Ok(profile);
        }

        // POST: api/trainer/trainerprofile
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(TrainerProfileGetResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProfile([FromForm] TrainerProfileCreateRequest request)
        {
            try
            {
                var profile = await _trainerProfileService.CreateProfileIfNotExistsAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = profile.Id }, profile);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
        }

        // PUT: api/trainer/trainerprofile/{id}
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfile(int id, [FromForm] TrainerProfileUpdateRequest request)
        {
            var ok = await _managerService.UpdateAsync(id, request);
            if (!ok) return NotFound(new ApiResponse(404, "Trainer profile not found."));
            return NoContent();
        }

        // DELETE: api/trainer/trainerprofile/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var result = await _trainerProfileService.DeleteProfileAsync(id);

            if (!result)
                return NotFound(new ApiResponse(404, "Trainer profile not found."));

            return NoContent();
        }

        // PUT: api/trainer/trainerprofile/status/{id}
        [HttpPut("Status/{id}")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(TrainerProfileGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            // Status update not implemented in current service layer
            return NotFound(new ApiResponse(404, "Endpoint not implemented: UpdateStatus."));
        }

        // DELETE: api/trainer/trainerprofile/status/{id}
        [HttpDelete("Status/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            // Status delete not implemented in current service layer
            return NotFound(new ApiResponse(404, "Endpoint not implemented: DeleteStatus."));
        }
    }
}
