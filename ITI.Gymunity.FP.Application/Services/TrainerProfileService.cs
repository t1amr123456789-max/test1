using AutoMapper;
using ITI.Gymunity.FP.Application.DTOs.Trainer;
using ITI.Gymunity.FP.Application.Specefications;
using ITI.Gymunity.FP.Domain;
using ITI.Gymunity.FP.Domain.Models.Trainer;
using ITI.Gymunity.FP.Domain.RepositoiesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Application.Services
{
    public class TrainerProfileService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<TrainerProfileResponse>> GetAllProfiles()
        {
            var profileSpecs = new TrainerWithUsersAndProgramsSpecs();

            var trainerProfiles = (await _unitOfWork.Repository<TrainerProfile, ITrainerProfileRepository>()
                                                   .GetAllWithSpecsAsync(profileSpecs))
                                                   .Select(tp => _mapper.Map<TrainerProfileResponse>(tp));

            return trainerProfiles;

        }

        public async Task<TrainerProfileGetResponse> CreateProfileIfNotExistsAsync(TrainerProfileCreateRequest request)
        {
            // Check unique Handle
            var existingHandle = (await _unitOfWork.Repository<TrainerProfile>().GetAllAsync()).Any(tp => tp.Handle == request.Handle);
            if (existingHandle)
                throw new InvalidOperationException("Handle already exists.");

            // Check user profile exists
            var existing = (await _unitOfWork.Repository<TrainerProfile>().GetAllAsync()).FirstOrDefault(tp => tp.UserId == request.UserId);
            if (existing != null)
                return _mapper.Map<TrainerProfileGetResponse>(existing);

            var entity = new TrainerProfile
            {
                UserId = request.UserId,
                Handle = request.Handle,
                Bio = request.Bio,
                CoverImageUrl = request.CoverImageUrl,
                VideoIntroUrl = request.VideoIntroUrl,
                BrandingColors = request.BrandingColors,
                YearsExperience = request.YearsExperience
            };

            _unitOfWork.Repository<TrainerProfile>().Add(entity);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                // log if you have logger
                throw;
            }

            return _mapper.Map<TrainerProfileGetResponse>(entity);
        }

        public async Task<bool> DeleteProfileAsync(int id)
        {
            var repo = _unitOfWork.Repository<TrainerProfile>();
            var entity = await repo.GetByIdAsync(id);
            if (entity == null) return false;
            repo.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
