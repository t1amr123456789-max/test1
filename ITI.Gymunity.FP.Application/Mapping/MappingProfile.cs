using AutoMapper;
using ITI.Gymunity.FP.Application.DTOs.Client;
using ITI.Gymunity.FP.Application.DTOs.Trainer;
using ITI.Gymunity.FP.Domain.Models.ProgramAggregate;
using ITI.Gymunity.FP.Domain.Models.Trainer;
using ITI.Gymunity.FP.Application.DTOs.ExerciseLibrary;
using ITI.Gymunity.FP.Application.DTOs.Program;
using ITI.Gymunity.FP.Application.DTOs.Chat;
using ITI.Gymunity.FP.Domain.Models.Messaging;
using ITI.Gymunity.FP.Domain.Models.Identity;
using ITI.Gymunity.FP.Application.DTOs.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create your mappings here
            CreateMap<TrainerProfile, TrainerProfileResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(tp => tp.User.UserName));

            CreateMap<TrainerProfile, DTOs.Client.TrainerClientResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(tp => tp.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(tp => tp.User.UserName));

            CreateMap<Program, ProgramGetAllResponse>()
                .ForMember(dest => dest.TrainerUserName, opt => opt.MapFrom(p => p.Trainer.UserName))
                .ForMember(dest => dest.TrainerHandle, opt => opt.MapFrom(p => p.TrainerProfile != null ? p.TrainerProfile.Handle : null));

            CreateMap<Program, ProgramGetByIdResponse>()
                .ForMember(dest => dest.TrainerUserName, opt => opt.MapFrom(p => p.Trainer.UserName))
                .ForMember(dest => dest.TrainerHandle, opt => opt.MapFrom(p => p.TrainerProfile != null ? p.TrainerProfile.Handle : null));

            CreateMap<ProgramWeek, ProgramWeekGetAllResponse>();
            CreateMap<ProgramDay, ProgramDayGetAllResponse>();
            CreateMap<ProgramDayExercise, ProgramDayExerciseGetAllResponse>();

            CreateMap<Exercise, ExerciseGetAllResponse>();
            CreateMap<Exercise, ExerciseGetByIdResponse>();
            CreateMap<ExerciseCreateRequest, Exercise>();
            CreateMap<ExerciseUpdateRequest, Exercise>();

            CreateMap<Message, MessageGetResponse>()
                .ForMember(dest => dest.ReadStatus, opt => opt.MapFrom(m => m.IsRead ? ITI.Gymunity.FP.Domain.Models.Messaging.MessageReadStatus.Seen : ITI.Gymunity.FP.Domain.Models.Messaging.MessageReadStatus.Delivered));

            CreateMap<Message, MessageSendResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(dest => dest.ReadStatus, opt => opt.MapFrom(m => m.IsRead ? ITI.Gymunity.FP.Domain.Models.Messaging.MessageReadStatus.Seen : ITI.Gymunity.FP.Domain.Models.Messaging.MessageReadStatus.Delivered));


            CreateMap<TrainerProfile, TrainerProfileGetResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(tp => tp.User.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(tp => tp.UserId));

            CreateMap<AppUser, ClientGetAllResponse>()
                .ForMember(d => d.UserId, o => o.MapFrom(u => u.Id))
                .ForMember(d => d.UserName, o => o.MapFrom(u => u.UserName));

            CreateMap<AppUser, ClientGetByIdResponse>()
                .ForMember(d => d.UserId, o => o.MapFrom(u => u.Id))
                .ForMember(d => d.UserName, o => o.MapFrom(u => u.UserName));

            CreateMap<Program, ProgramAdminGetResponse>()
                .ForMember(dest => dest.TrainerUserName, opt => opt.MapFrom(p => p.Trainer.UserName));

            CreateMap<Exercise, ExerciseAdminGetResponse>();
            CreateMap<TrainerProfile, TrainerAdminGetResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(tp => tp.User.UserName));
        }
    }
}
