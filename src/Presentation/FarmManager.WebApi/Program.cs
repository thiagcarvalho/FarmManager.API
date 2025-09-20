using AutoMapper;
using FarmManager.Application;
using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Services;
using FarmManager.Domain.AnimalFactory;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Persistence.Command;
using FarmManager.Persistence.Command.Store;
using FarmManager.Persistence.Query;
using FarmManager.Persistence.Query.Store;
using FarmManager.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile),
    typeof(CommandMappingProfile));

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddRepositoryServices(builder.Configuration);
builder.Services.AddFactories(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LoggerMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
