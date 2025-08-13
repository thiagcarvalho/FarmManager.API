using AutoMapper;
using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Services;
using FarmManager.Persistence.Query.Store;
using FarmManager.Persistence.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAnimalQueryRepository, AnimalQueryRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


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
