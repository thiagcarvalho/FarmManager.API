using AutoMapper;
using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Services;
using FarmManager.Domain.AnimalFactory;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Persistence.Command;
using FarmManager.Persistence.Command.Store;
using FarmManager.Persistence.EF.Context;
using FarmManager.Persistence.Query;
using FarmManager.Persistence.Query.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace FarmManager.Application;

public static class Config
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAnimalService, AnimalService>();
        services.AddAutoMapper(typeof(QuerryMappingProfile), typeof(CommandMappingProfile));
    }

    public static void AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddDbContext<FarmManagerDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")));

        services.AddScoped<IAnimalQueryRepository, AnimalQueryRepository>();
        services.AddScoped<IAnimalCommandRepository, AnimalCommandRepository>();
    }

    public static void AddFactories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAnimalFactory, AnimalFactory>();
    }
}
