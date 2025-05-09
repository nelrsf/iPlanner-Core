using iPlanner.Core.Application.DTO.Calendar;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Core.Application.Mappers;
using iPlanner.Core.Application.Services;
using iPlanner.Core.Application.Services.Calendar;
using iPlanner.Core.Application.Services.Locations;
using iPlanner.Core.Entities.Calendar;
using iPlanner.Core.Infrastructure.Data;
using iPlanner.Core.Infrastructure.Repositories.Calendar;
using iPlanner.Infrastructure.Locations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Mappers
builder.Services.AddScoped<IMapper<CalendarDayDTO, CalendarDay>, CalendarDayMapper>();

// Repositories
builder.Services.AddScoped<ILocationsRepository, ExternalLibraryLocationsRepository>();
builder.Services.AddScoped<ICalendarRepository, EFCCalendarRepository>();

// Services
builder.Services.AddScoped<CalendarDateCalculator>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICalendarService, CalendarService>();


builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
