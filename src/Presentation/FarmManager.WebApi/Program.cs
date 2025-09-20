using AutoMapper;
using FarmManager.Application;
using FarmManager.Persistence.Command;
using FarmManager.Persistence.Query;
using FarmManager.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(QuerryMappingProfile),
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
