using AutoMapper;
using ITI.Gymunity.FP.Application.DTOs.Admin;
using ITI.Gymunity.FP.Domain.RepositoiesContracts;
using ITI.Gymunity.FP.Domain.Models.ProgramAggregate;
using ITI.Gymunity.FP.Domain.Models.Trainer;
using ITI.Gymunity.FP.Domain;
using Microsoft.AspNetCore.Identity;
using ITI.Gymunity.FP.Domain.Models.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Application.Services
{
 public interface IAdminService
 {
 Task<IReadOnlyList<ProgramAdminGetResponse>> GetAllPendingProgramsAsync();
 Task<bool> ApproveProgramAsync(int programId, string adminId);
 Task<bool> RejectProgramAsync(int programId, string adminId, string reason);
 Task<bool> DeleteProgramAsync(int programId);

 Task<IReadOnlyList<ExerciseAdminGetResponse>> GetAllPendingExercisesAsync();
 Task<bool> ApproveExerciseAsync(int exerciseId, string adminId);
 Task<bool> RejectExerciseAsync(int exerciseId, string adminId, string reason);
 Task<bool> DeleteExerciseAsync(int exerciseId);

 Task<IReadOnlyList<TrainerAdminGetResponse>> GetAllPendingTrainersAsync();
 Task<bool> VerifyTrainerAsync(int trainerProfileId, string adminId);
 Task<bool> RejectTrainerAsync(int trainerProfileId, string adminId, string reason);
 Task<bool> DeleteTrainerAccountAsync(int trainerProfileId);
 }

 public class AdminService : IAdminService
 {
 private readonly IProgramRepository _programRepo;
 private readonly IExerciseLibraryRepository _exerciseRepo;
 private readonly ITrainerProfileRepository _trainerRepo;
 private readonly IUnitOfWork _unitOfWork;
 private readonly IMapper _mapper;
 private readonly UserManager<AppUser> _userManager;

 public AdminService(IProgramRepository programRepo, IExerciseLibraryRepository exerciseRepo, ITrainerProfileRepository trainerRepo, IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
 {
 _programRepo = programRepo;
 _exerciseRepo = exerciseRepo;
 _trainerRepo = trainerRepo;
 _unitOfWork = unitOfWork;
 _mapper = mapper;
 _userManager = userManager;
 }

 public async Task<IReadOnlyList<ProgramAdminGetResponse>> GetAllPendingProgramsAsync()
 {
 // Assuming a Status column exists; since models can't change, treat IsPublic=false and IsDeleted=false as pending (temporary)
 var list = await _programRepo.GetAllAsync();
 var pending = list.Where(p => !p.IsDeleted && !p.IsPublic).ToList();
 return pending.Select(p => _mapper.Map<ProgramAdminGetResponse>(p)).ToList();
 }

 public async Task<bool> ApproveProgramAsync(int programId, string adminId)
 {
 var p = await _programRepo.GetByIdAsync(programId);
 if (p == null) return false;
 p.IsPublic = true; // mark approved
 _programRepo.Update(p);
 await _unitOfWork.CompleteAsync();
 return true;
 }

 public async Task<bool> RejectProgramAsync(int programId, string adminId, string reason)
 {
 var p = await _programRepo.GetByIdAsync(programId);
 if (p == null) return false;
 p.IsDeleted = true; // soft reject -> delete
 _programRepo.Update(p);
 await _unitOfWork.CompleteAsync();
 return true;
 }

 public async Task<bool> DeleteProgramAsync(int programId)
 {
 var p = await _programRepo.GetByIdAsync(programId);
 if (p == null) return false;
 _programRepo.Delete(p);
 await _unitOfWork.CompleteAsync();
 return true;
 }

 public async Task<IReadOnlyList<ExerciseAdminGetResponse>> GetAllPendingExercisesAsync()
 {
 var list = await _exerciseRepo.GetAllAsync();
 var pending = list.Where(e => e.IsCustom && !e.IsDeleted && string.IsNullOrEmpty(e.ThumbnailUrl)).ToList();
 return pending.Select(e => _mapper.Map<ExerciseAdminGetResponse>(e)).ToList();
 }

 public async Task<bool> ApproveExerciseAsync(int exerciseId, string adminId)
 {
 var e = await _exerciseRepo.GetByIdAsync(exerciseId);
 if (e == null) return false;
 // mark approved: use ThumbnailUrl as marker (temporary)
 e.ThumbnailUrl ??= "approved";
 _exerciseRepo.Update(e);
 await _unitOfWork.CompleteAsync();
 return true;
 }

 public async Task<bool> RejectExerciseAsync(int exerciseId, string adminId, string reason)
 {
 var e = await _exerciseRepo.GetByIdAsync(exerciseId);
 if (e == null) return false;
 _exerciseRepo.Delete(e);
 await _unitOfWork.CompleteAsync();
 return true;
 }

 public async Task<bool> DeleteExerciseAsync(int exerciseId)
 {
 var e = await _exerciseRepo.GetByIdAsync(exerciseId);
 if (e == null) return false;
 _exerciseRepo.Delete(e);
 await _unitOfWork.CompleteAsync();
 return true;
 }

 public async Task<IReadOnlyList<TrainerAdminGetResponse>> GetAllPendingTrainersAsync()
 {
 var list = await _trainerRepo.GetAllWithSpecsAsync(new ITI.Gymunity.FP.Application.Specefications.TrainerWithUsersAndProgramsSpecs());
 var pending = list.Where(t => !t.IsVerified).ToList();
 return pending.Select(t => _mapper.Map<TrainerAdminGetResponse>(t)).ToList();
 }

 public async Task<bool> VerifyTrainerAsync(int trainerProfileId, string adminId)
 {
 var t = await _trainerRepo.GetByIdAsync(trainerProfileId);
 if (t == null) return false;
 t.IsVerified = true;
 _trainerRepo.Update(t);
 await _unitOfWork.CompleteAsync();

 // mark user as verified
 var user = await _userManager.FindByIdAsync(t.UserId);
 if (user != null) { user.IsVerified = true; await _userManager.UpdateAsync(user); }
 return true;
 }

 public async Task<bool> RejectTrainerAsync(int trainerProfileId, string adminId, string reason)
 {
 var t = await _trainerRepo.GetByIdAsync(trainerProfileId);
 if (t == null) return false;
 _trainerRepo.Delete(t);
 await _unitOfWork.CompleteAsync();
 return true;
 }

 public async Task<bool> DeleteTrainerAccountAsync(int trainerProfileId)
 {
 var t = await _trainerRepo.GetByIdAsync(trainerProfileId);
 if (t == null) return false;
 var user = await _userManager.FindByIdAsync(t.UserId);
 if (user != null) await _userManager.DeleteAsync(user);
 _trainerRepo.Delete(t);
 await _unitOfWork.CompleteAsync();
 return true;
 }
 }
}
