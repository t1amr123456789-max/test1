using ITI.Gymunity.FP.Application.DTOs.Client;
using ITI.Gymunity.FP.Application.Contracts.ExternalServices;
using ITI.Gymunity.FP.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ITI.Gymunity.FP.Domain;
using ITI.Gymunity.FP.Domain.Models.Trainer;

namespace ITI.Gymunity.FP.APIs.Account
{
 [Route("api/account")]
 [ApiController]
 public class AccountController : ControllerBase
 {
 private readonly UserManager<AppUser> _userManager;
 private readonly SignInManager<AppUser> _signInManager;
 private readonly IAuthService _authService;
 private readonly IUnitOfWork _unitOfWork;

 public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService, IUnitOfWork unitOfWork)
 {
 _userManager = userManager;
 _signInManager = signInManager;
 _authService = authService;
 _unitOfWork = unitOfWork;
 }

 [HttpPost("register")]
 public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
 {
 var user = new AppUser { UserName = request.UserName, Email = request.Email, FullName = request.FullName, Role = request.Role };
 var res = await _userManager.CreateAsync(user, request.Password);
 if (!res.Succeeded) return BadRequest(res.Errors.Select(e => e.Description));
 await _userManager.AddToRoleAsync(user, request.Role.ToString());

 // If the registered user is a Trainer, create a TrainerProfile so clients can find them
 if (request.Role.ToString() == "Trainer")
 {
 var profile = new TrainerProfile
 {
 UserId = user.Id,
 Handle = user.UserName ?? user.Id,
 Bio = string.Empty,
 IsVerified = false,
 YearsExperience =0
 };
 _unitOfWork.Repository<TrainerProfile>().Add(profile);
 await _unitOfWork.CompleteAsync();
 }

 var token = await _authService.CreateTokenAsync(user, _userManager);
 return Ok(new { token });
 }

 [HttpPost("login")]
 public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
 {
 var user = await _userManager.FindByNameAsync(request.UserName ?? request.Email);
 if (user == null) return Unauthorized();
 var sign = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
 if (!sign.Succeeded) return Unauthorized();
 var token = await _authService.CreateTokenAsync(user, _userManager);
 return Ok(new { token, role = user.Role, userId = user.Id });
 }
 }
}
