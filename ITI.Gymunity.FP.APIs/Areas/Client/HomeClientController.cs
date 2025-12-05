    using ITI.Gymunity.FP.Application.DTOs.Client;
    using ITI.Gymunity.FP.Application.Specefications;
    using ITI.Gymunity.FP.Domain;
    using ITI.Gymunity.FP.Domain.Models.Trainer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using DomainProgram = ITI.Gymunity.FP.Domain.Models.ProgramAggregate.Program;

    namespace ITI.Gymunity.FP.APIs.Areas.Client
    {
             [Area("Client")]
             [Route("api/client/[controller]")]
             [ApiController]
             public class HomeClientController : ControllerBase
             {
             private readonly IUnitOfWork _unitOfWork;
             private readonly AutoMapper.IMapper _mapper;

             public HomeClientController(IUnitOfWork unitOfWork, AutoMapper.IMapper mapper)
             {
             _unitOfWork = unitOfWork;
             _mapper = mapper;
             }

             // GET: api/client/homeclient/search?term=xyz
             [HttpGet("search")]
             public async Task<ActionResult> Search([FromQuery] string term)
             {
             if (string.IsNullOrWhiteSpace(term))
             return BadRequest("Search term is required.");

             // Search programs by title or trainer name/handle
             var programSpec = new ProgramWithTrainerSpec(term);
             var programs = await _unitOfWork.Repository<DomainProgram>().ListAsync(programSpec);
             var programDtos = programs.Select(p => _mapper.Map<ProgramClientResponse>(p)).ToList();

             // Search trainers by handle or user full name
             var trainerSpec = new TrainerWithUsersAndProgramsSpecs(tp => tp.Handle.Contains(term) || tp.User.FullName.Contains(term));
             var trainers = await _unitOfWork.Repository<TrainerProfile, ITI.Gymunity.FP.Domain.RepositoiesContracts.ITrainerProfileRepository>().GetAllWithSpecsAsync(trainerSpec);
             var trainerDtos = trainers.Select(t => _mapper.Map<TrainerClientResponse>(t)).ToList();

             return Ok(new { programs = programDtos, trainers = trainerDtos });
             }

             // GET: api/client/homeclient/programs
             [HttpGet("programs")]
             public async Task<ActionResult<IEnumerable<ProgramClientResponse>>> GetAllPrograms()
             {
             var spec = new ProgramWithTrainerSpec();
             var programs = await _unitOfWork.Repository<DomainProgram>().ListAsync(spec);
             return Ok(_mapper.Map<IReadOnlyList<ProgramClientResponse>>(programs));
             }

             // GET: api/client/homeclient/programs/{id}
             [HttpGet("programs/{id:int}")]
             public async Task<ActionResult<ProgramClientResponse>> GetProgramById(int id)
             {
             var spec = new ProgramWithTrainerSpec();
             // create a criteria wrapper spec
             spec.Criteria = p => p.Id == id;
             var program = (await _unitOfWork.Repository<DomainProgram>().ListAsync(spec)).FirstOrDefault();
             if (program is null) return NotFound();
             return Ok(_mapper.Map<ProgramClientResponse>(program));
             }

             // GET: api/client/homeclient/trainers
             [HttpGet("trainers")]
             public async Task<ActionResult<IEnumerable<TrainerClientResponse>>> GetAllTrainers()
             {
             var spec = new TrainerWithUsersAndProgramsSpecs();
             var trainers = await _unitOfWork.Repository<TrainerProfile, ITI.Gymunity.FP.Domain.RepositoiesContracts.ITrainerProfileRepository>().GetAllWithSpecsAsync(spec);
             return Ok(trainers.Select(t => _mapper.Map<TrainerClientResponse>(t)));
             }

             // GET: api/client/homeclient/trainers/{id}
             [HttpGet("trainers/{id:int}")]
             public async Task<ActionResult<TrainerClientResponse>> GetTrainerById(int id)
             {
             var spec = new TrainerWithUsersAndProgramsSpecs(tp => tp.Id == id);
             var trainer = await _unitOfWork.Repository<TrainerProfile, ITI.Gymunity.FP.Domain.RepositoiesContracts.ITrainerProfileRepository>().GetWithSpecsAsync(spec);
             if (trainer is null) return NotFound();
             return Ok(_mapper.Map<TrainerClientResponse>(trainer));
             }
             }
    }
