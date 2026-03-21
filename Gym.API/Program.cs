using System.Text.Json.Serialization;
using Gym.API.Middleware;
using Gym.Application;
using Gym.Application.Interfaces.UnitOfWork;
using Gym.Infrastructure;
using Gym.Infrastructure.Data;
using Gym.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<GymDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplication();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<GlobalExceptionMiddleware>();

// Configure JSON serialization to use string representation for enums, which will make the API responses more readable and user-friendly.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter());
    });

// Configure Swagger to use inline definitions for enums, which will display enum values as strings in the Swagger UI.
builder.Services.AddSwaggerGen(o =>
{
    o.UseInlineDefinitionsForEnums();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();


