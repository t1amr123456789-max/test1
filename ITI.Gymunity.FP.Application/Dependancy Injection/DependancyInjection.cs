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

            // Application Services
            services.AddScoped<TrainerProfileService>();
            services.AddScoped<IProgramService, ProgramService>();
            services.AddScoped<IExerciseLibraryService, ExerciseLibraryService>();
            services.AddScoped<IProgramManagerService, ProgramManagerService>();
            services.AddScoped<IWeekService, WeekService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<IDayExerciseService, DayExerciseService>();
            services.AddScoped<ITrainerProfileManagerService, TrainerProfileManagerService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IUserAdminService, UserAdminService>();
            services.AddScoped<ISubscriptionAdminService, SubscriptionAdminService>();

            return services;
        }
    }
}
