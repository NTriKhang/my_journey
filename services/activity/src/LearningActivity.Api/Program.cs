internal class Program
{
    private static void Main(string[] args)
    {
		try
		{
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    [LearningActivity.Application.ApplicationAssembly.Assembly]
                );
            });

            builder.Services.AddAutoMapper((sp, cfg) => { }, LearningActivity.Application.ApplicationAssembly.Assembly);
            LearningActivity.Infrastructure.ServiceCollectionExtensions.AddInfrastructure(builder.Services, builder.Configuration);

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        catch (System.Reflection.ReflectionTypeLoadException ex)
        {
            foreach (var loaderEx in ex.LoaderExceptions)
            {
                Console.WriteLine(loaderEx?.Message);
            }
            throw;
        }
    }
}

