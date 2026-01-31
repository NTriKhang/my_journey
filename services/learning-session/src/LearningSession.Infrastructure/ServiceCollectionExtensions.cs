using System;
using LearningSession.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningSession.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("LearningSession") ?? configuration["ConnectionStrings:LearningSession"];
            services.AddDbContext<LearningSessionDbContext>(opt => opt.UseNpgsql(conn));
            services.AddScoped<Application.Repositories.ILearningSessionRepository, Repositories.LearningSessionRepository>();
        }
    }
}

