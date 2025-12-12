using ITI.Gymunity.FP.Application.Mapping;
using ITI.Gymunity.FP.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Gymunity.FP.Application.Dependancy_Injection
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register your application services here => سجل ال Services الخاصة بالتطبيق هنا

            // Auto Mapper Configurations
            services.AddAutoMapper((opt) => { }, typeof(MappingProfile).Assembly);

            // Trainer services
            services.AddScoped<TrainerProfileService>();
            services.AddScoped<ITrainerProfileManagerService, TrainerProfileManagerService>();

            // Home Client
            services.AddScoped<IHomeClientService, HomeClientService>();

            // Day - Week - Program services
            services.AddScoped<IDayExerciseService, DayExerciseService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<IWeekService, WeekService>();
            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IProgramManagerService, ProgramManagerService>();

            // Exercise Library
            services.AddScoped<IExerciseLibraryService, ExerciseLibraryService>();

            // Chat
            services.AddScoped<IChatService, ChatService>();

            // Admin Services
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserAdminService, UserAdminService>();
            services.AddScoped<ISubscriptionAdminService, SubscriptionAdminService>();

            return services;
        }
    }
}
