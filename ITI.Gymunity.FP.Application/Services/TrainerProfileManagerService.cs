using AutoMapper;
using ITI.Gymunity.FP.Application.DTOs.Trainer;
using ITI.Gymunity.FP.Domain.RepositoiesContracts;
using ITI.Gymunity.FP.Domain.Models.Trainer;
using ITI.Gymunity.FP.Domain;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Application.Services
{
 public interface ITrainerProfileManagerService
 {
 Task<TrainerProfileGetResponse?> GetByIdAsync(int id);
 Task<bool> UpdateAsync(int id, TrainerProfileUpdateRequest request);
 }

 public class TrainerProfileManagerService : ITrainerProfileManagerService
 {
 private readonly ITrainerProfileRepository _repo;
 private readonly IUnitOfWork _unitOfWork;
 private readonly IMapper _mapper;

 public TrainerProfileManagerService(ITrainerProfileRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
 {
 _repo = repo;
 _unitOfWork = unitOfWork;
 _mapper = mapper;
 }

 public async Task<TrainerProfileGetResponse?> GetByIdAsync(int id)
 {
 var entity = await _repo.GetWithSpecsAsync(new ITI.Gymunity.FP.Application.Specefications.TrainerWithUsersAndProgramsSpecs(tp => tp.Id == id));
 if (entity == null) return null;
 return _mapper.Map<TrainerProfileGetResponse>(entity);
 }

 public async Task<bool> UpdateAsync(int id, TrainerProfileUpdateRequest request)
 {
 var entity = await _repo.GetByIdAsync(id);
 if (entity == null) return false;
 entity.Handle = request.Handle;
 entity.Bio = request.Bio;
 entity.CoverImageUrl = request.CoverImageUrl;
 entity.VideoIntroUrl = request.VideoIntroUrl;
 entity.BrandingColors = request.BrandingColors;
 entity.YearsExperience = request.YearsExperience;
 _repo.Update(entity);
 await _unitOfWork.CompleteAsync();
 return true;
 }
 }
}
