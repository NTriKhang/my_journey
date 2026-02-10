using System.Reflection;
using MediatR;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure CORS to allow requests from any origin (adjust for production)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add Swagger/OpenAPI services
// Generates OpenAPI document and enables Swagger UI in development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MediatR by scanning the Application assembly (marker type)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        [LearningSession.Application.ApplicationAssembly.Assembly]
    );
});

// Register AutoMapper and scan profiles from the Application assembly
// Use the assembly containing the marker type so AutoMapper picks up profiles
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(typeof(LearningSession.Application.Mappings.LearningSessionProfile));
});

// Allow Infrastructure to register its services (DbContext, repositories)
LearningSession.Infrastructure.ServiceCollectionExtensions.AddInfrastructure(builder.Services, builder.Configuration);

// (Optional) Existing convenience extension for OpenAPI if present in project
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable generated Swagger document and UI when running in Development
    app.UseSwagger();
    app.UseSwaggerUI();

    // If the project also uses a custom OpenAPI helper, keep mapping it.
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Enable CORS using configured policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
