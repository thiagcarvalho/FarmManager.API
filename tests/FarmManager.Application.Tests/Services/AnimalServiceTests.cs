using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using FarmManager.Application.Services;
using FarmManager.Domain.Interfaces.Factories;
using Moq;

namespace FarmManager.Application.Tests.Services;

public class AnimalServiceTests : AnimalServiceTestBase
{
    

    [Fact]
    public void GetAnimal_WithValidId_ReturnsAnimalViewModel()
    {
        //Arrange
        var expectedAnimal = new AnimalViewModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 1,
            Age = 5,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow"
        };

        MockQueryRepository
            .Setup(repo => repo.GetAnimal(It.IsAny<Guid>()))
            .Returns(expectedAnimal);

        //Act

        var result = AnimalService.GetAnimal(expectedAnimal.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedAnimal.Id, result.Id);
        Assert.Equal(expectedAnimal.RegisterNumber, result.RegisterNumber);
        Assert.Equal(expectedAnimal.Age, result.Age);
        Assert.Equal(expectedAnimal.Weight, result.Weight);
        Assert.Equal(expectedAnimal.Birthday, result.Birthday);
        Assert.Equal(expectedAnimal.Type, result.Type);

        MockQueryRepository.Verify(x => x.GetAnimal(expectedAnimal.Id), Times.Once);   
    }

    [Fact]
    public void GetAnimal_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var animalId = Guid.NewGuid();

        MockQueryRepository
            .Setup(x => x.GetAnimal(animalId))
            .Returns((AnimalViewModel?)null);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.GetAnimal(animalId));

        Assert.Equal($"Entity \"Animal\" ({animalId}) was not found.", exception.Message);
        MockQueryRepository.Verify(x => x.GetAnimal(animalId), Times.Once);
    }
}