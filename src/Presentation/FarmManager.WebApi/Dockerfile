# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build
ARG BUILD_CONFIGURATION=Release
ARG CONNECTIONSTRINGS__POSTGRES

WORKDIR /src
COPY ["src/Presentation/FarmManager.WebApi/FarmManager.WebApi.csproj", "src/Presentation/FarmManager.WebApi/"]
COPY ["src/Core/Application/FarmManager.Application.Contracts/FarmManager.Application.Contracts.csproj", "src/Core/Application/FarmManager.Application.Contracts/"]
COPY ["src/Core/Domain/FarmManager.Domain/FarmManager.Domain.csproj", "src/Core/Domain/FarmManager.Domain/"]
COPY ["src/Core/Application/FarmManager.Application/FarmManager.Application.csproj", "src/Core/Application/FarmManager.Application/"]
COPY ["src/Persistence/FarmManager.Persistence.Command/FarmManager.Persistence.Command.csproj", "src/Persistence/FarmManager.Persistence.Command/"]
COPY ["src/Persistence/FarmManager.Persistence.DataModels/FarmManager.Persistence.DataModels.csproj", "src/Persistence/FarmManager.Persistence.DataModels/"]
COPY ["src/Persistence/FarmManager.Persistence.EF/FarmManager.Persistence.EF.csproj", "src/Persistence/FarmManager.Persistence.EF/"]
COPY ["src/Persistence/FarmManager.Persistence.Query/FarmManager.Persistence.Query.csproj", "src/Persistence/FarmManager.Persistence.Query/"]
RUN dotnet restore "./src/Presentation/FarmManager.WebApi/FarmManager.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Presentation/FarmManager.WebApi"
RUN dotnet build "./FarmManager.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FarmManager.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
ARG CONNECTIONSTRINGS__POSTGRES
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FarmManager.WebApi.dll"]