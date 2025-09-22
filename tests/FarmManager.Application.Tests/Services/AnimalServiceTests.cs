using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using FarmManager.Application.Services;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Domain.ValueObject;
using Moq;
using System.ComponentModel.DataAnnotations;

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

    [Fact]
    public void GetAllAnimals_ReturnsListOfAnimalViewModels()
    {
        // Arrange
        var expectedAnimals = new List<AnimalViewModel>
        {
            new AnimalViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 1,
                Age = 5,
                Weight = 150.5m,
                Birthday = new DateTime(2020, 1, 1),
                Type = "Cow"
            },
            new AnimalViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 2,
                Age = 3,
                Weight = 120.0m,
                Birthday = new DateTime(2025, 1, 1),
                Type = "Calf"
            },
            new AnimalViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 3,
                Age = 4,
                Weight = 160.0m,
                Birthday = new DateTime(2020, 1, 1),
                Type = "Bull"
            }
        };

        MockQueryRepository
            .Setup(repo => repo.GetAllAnimals())
            .Returns(expectedAnimals);

        // Act
        var result = AnimalService.GetAllAnimals();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedAnimals.Count, result.Count);
        Assert.Equal(expectedAnimals, result);
        MockQueryRepository.Verify(x => x.GetAllAnimals(), Times.Once);
    }

    [Fact]
    public void GettAllAnimals_ReturnsEmptyList()
    {
        // Arrange
        var expectedAnimals = new List<AnimalViewModel>();
        MockQueryRepository
            .Setup(repo => repo.GetAllAnimals())
            .Returns(expectedAnimals);

        // Act
        var result = AnimalService.GetAllAnimals();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        MockQueryRepository.Verify(x => x.GetAllAnimals(), Times.Once);
    }

    [Fact]
    public void SaveAnimal_WithValidInput_ReturnsNewAnimalId()
    {
        // Arrange
        var newAnimalId = Guid.NewGuid();

        var animalInputModel = new AnimalInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow"
        };

        MockCommandRepository
            .Setup(repo => repo.SaveAnimal(It.IsAny<Domain.Entities.Animal>()))
            .Returns(newAnimalId);

        // Act
        var result = AnimalService.SaveAnimal(animalInputModel);

        // Assert
        Assert.Equal(newAnimalId, result);
    }

    [Fact]
    public void SaveAnimal_WithDuplicateRegisterNumber_ThrowsValidationException()
    {
        // Arrange
        var animalInputModel = new AnimalInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow"
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(animalInputModel.RegisterNumber))
            .Returns(true);

        // Act & Assert
        var exception = Assert.Throws<DuplicateResourceException>(() => AnimalService.SaveAnimal(animalInputModel));

        Assert.Equal($"Animal with identifier '{animalInputModel.RegisterNumber}' already exists.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(animalInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.SaveAnimal(It.IsAny < Domain.Entities.Animal>()), Times.Never);
    }

    [Fact]
    public void UpdateAnimal_WithValidInput_CallsUpdateAnimal()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var animalInputModel = new AnimalInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow"
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(animalInputModel.RegisterNumber))
            .Returns(true);

        // Act
        AnimalService.UpdateAnimal(animalId, animalInputModel);

        // Assert
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(animalInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateAnimal(animalId, It.IsAny<Domain.Entities.Animal>()), Times.Once);
    }

    [Fact]
    public void UpdateAnimal_WithNonExistentRegisterNumber_ThrowsNotFoundException()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var animalInputModel = new AnimalInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow"
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(animalInputModel.RegisterNumber))
            .Returns(false);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.UpdateAnimal(animalId, animalInputModel));
        Assert.Equal($"The Animal with register number {animalInputModel.RegisterNumber} does not exist.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(animalInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateAnimal(animalId, It.IsAny<Domain.Entities.Animal>()), Times.Never);
    }

    [Fact]
    public void DeleteAnimal_WithValidId_CallsDeleteAnimal()
    {
        // Arrange
        var animalId = Guid.NewGuid();

        // Act
        AnimalService.DeleteAnimal(animalId);

        // Assert
        MockCommandRepository.Verify(x => x.DeleteAnimal(animalId), Times.Once);
    }
}