using ITI.Gymunity.FP.Application.DTOs.Client;
using ITI.Gymunity.FP.Application.Specefications;
using ITI.Gymunity.FP.Domain.Models.Trainer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.APIs.Areas.Client
{
 [Area("Client")]
 [Route("api/client/[controller]")]
 [ApiController]
 public class HomeClientController : ControllerBase
 {
 private readonly ITI.Gymunity.FP.Application.Services.IHomeClientService _homeService;

 public HomeClientController(ITI.Gymunity.FP.Application.Services.IHomeClientService homeService)
 {
 _homeService = homeService;
 }

 // GET: api/client/homeclient/search?term=xyz
 [HttpGet("search")]
 public async Task<ActionResult> Search([FromQuery] string term)
 {
 if (string.IsNullOrWhiteSpace(term))
 return BadRequest("Search term is required.");

 var (programs, trainers) = await _homeService.SearchAsync(term);
 return Ok(new { programs, trainers });
 }

 // GET: api/client/homeclient/programs
 [HttpGet("programs")]
 public async Task<ActionResult<IEnumerable<ProgramClientResponse>>> GetAllPrograms()
 {
 var programs = await _homeService.GetAllProgramsAsync();
 return Ok(programs);
 }

 // GET: api/client/homeclient/programs/{id}
 [HttpGet("programs/{id:int}")]
 public async Task<ActionResult<ProgramClientResponse>> GetProgramById(int id)
 {
 var program = await _homeService.GetProgramByIdAsync(id);
 if (program is null) return NotFound();
 return Ok(program);
 }

 // GET: api/client/homeclient/trainers
 [HttpGet("trainers")]
 public async Task<ActionResult<IEnumerable<TrainerClientResponse>>> GetAllTrainers()
 {
 var trainers = await _homeService.GetAllTrainersAsync();
 return Ok(trainers);
 }

 // GET: api/client/homeclient/trainers/{id}
 [HttpGet("trainers/{id:int}")]
 public async Task<ActionResult<TrainerClientResponse>> GetTrainerById(int id)
 {
 var trainer = await _homeService.GetTrainerByIdAsync(id);
 if (trainer is null) return NotFound();
 return Ok(trainer);
 }
 }
}
