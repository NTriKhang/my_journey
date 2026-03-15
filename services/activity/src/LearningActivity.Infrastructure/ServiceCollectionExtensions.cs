using System;
using LearningActivity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LearningActivity.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration.GetConnectionString("LearningActivity") ?? configuration["ConnectionStrings:LearningActivity"];
            services.AddDbContext<LearningActivityDbContext>(opt => opt.UseNpgsql(conn));
            services.AddScoped<Application.Repositories.ILearningActivityRepository, Repositories.LearningActivityRepository>();
        }
    }
}

