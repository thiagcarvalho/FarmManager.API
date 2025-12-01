using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using FarmManager.Domain.Entities;
using Moq;

namespace FarmManager.Application.Tests.Services;

public class AnimalServiceCalfTests : AnimalServiceTestBase
{
    [Fact]
    public void GetCalf_WithValidId_ReturnsCalfViewModel()
    {
        //Arrange
        var expectedCalf = new CalfViewModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 10,
            Age = 5,
            Weight = 150.5m,
            Birthday = new DateTime(2025, 1, 1),
            Type = "Calf",
            Gender = true,
            MotherNumber = 10
        };

        MockQueryRepository
            .Setup(repo => repo.GetCalf(It.IsAny<Guid>()))
            .Returns(expectedCalf);

        //Act

        var result = AnimalService.GetCalf(expectedCalf.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCalf.Id, result.Id);
        Assert.Equal(expectedCalf.RegisterNumber, result.RegisterNumber);
        Assert.Equal(expectedCalf.Age, result.Age);
        Assert.Equal(expectedCalf.Weight, result.Weight);
        Assert.Equal(expectedCalf.Birthday, result.Birthday);
        Assert.Equal(expectedCalf.Type, result.Type);
        Assert.Equal(expectedCalf.Gender, result.Gender);
        Assert.Equal(expectedCalf.MotherNumber, result.MotherNumber);

        MockQueryRepository.Verify(x => x.GetCalf(expectedCalf.Id), Times.Once);
    }

    [Fact]
    public void GetCow_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var calfId = Guid.NewGuid();

        MockQueryRepository
            .Setup(x => x.GetAnimal(calfId))
            .Returns((CalfViewModel?)null);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.GetCalf(calfId));

        Assert.Equal($"Entity \"Calf\" ({calfId}) was not found.", exception.Message);
        MockQueryRepository.Verify(x => x.GetCalf(calfId), Times.Once);
    }

    [Fact]
    public void GetAllCalves_ReturnsListOfCalfViewModels()
    {
        // Arrange
        var expectedCalves = new List<CalfViewModel>
        {
            new CalfViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 10,
                Age = 5,
                Weight = 150.5m,
                Birthday = new DateTime(2020, 1, 1),
                Type = "Calf",
                Gender = true,
                MotherNumber = 10
            },
            new CalfViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 11,
                Age = 3,
                Weight = 120.0m,
                Birthday = new DateTime(2025, 6, 15),
                Type = "Calf",
                Gender = false,
                MotherNumber = 11
            },
            new CalfViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 12,
                Age = 1,
                Weight = 100.0m,
                Birthday = new DateTime(2026, 3, 10),
                Type = "Calf",
                Gender = false,
                MotherNumber = 12
            }
        };

        MockQueryRepository
            .Setup(repo => repo.GetAllCalves())
            .Returns(expectedCalves);

        // Act
        var result = AnimalService.GetAllCalves();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCalves.Count, result.Count);
        Assert.Equal(expectedCalves, result);
        MockQueryRepository.Verify(x => x.GetAllCalves(), Times.Once);
    }

    [Fact]
    public void GettAllCalves_ReturnsEmptyList()
    {
        // Arrange
        var expectedCalf = new List<CalfViewModel>();
        MockQueryRepository
            .Setup(repo => repo.GetAllCalves())
            .Returns(expectedCalf);

        // Act
        var result = AnimalService.GetAllCalves();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        MockQueryRepository.Verify(x => x.GetAllCalves(), Times.Once);
    }

    [Fact]
    public void SaveCalf_WithValidInput_ReturnsNewCowId()
    {
        // Arrange
        var newCalfId = Guid.NewGuid();

        var calfInputModel = new CalfInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2025, 1, 1),
            Type = "Calf",
            Gender = true,
            MotherNumber = 1
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"))
            .Returns(true);

        MockCommandRepository
            .Setup(repo => repo.SaveCalf(It.IsAny<Calf>()))
            .Returns(newCalfId);

        // Act
        var result = AnimalService.SaveCalf(calfInputModel);

        // Assert
        Assert.Equal(newCalfId, result);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumberAndType(calfInputModel.RegisterNumber, "Cow"), Times.Once);
        MockCommandRepository.Verify(x => x.SaveCalf(It.IsAny<Calf>()), Times.Once);
    }

    [Fact]
    public void SaveCalf_WithDuplicateRegisterNumber_ThrowsValidationException()
    {
        // Arrange
        var calfInputModel = new CalfInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Calf",
            Gender = true,
            MotherNumber = 1
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"))
            .Returns(false);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.SaveCalf(calfInputModel));
        Assert.Equal($"A vaca com o numero de registro {calfInputModel.MotherNumber} nao existe.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"), Times.Once);
        MockCommandRepository.Verify(x => x.SaveCalf(It.IsAny<Calf>()), Times.Never);
    }

    [Fact]
    public void UpdateCalf_WithValidInput_CallsUpdateCalf()
    {
        // Arrange
        var calfId = Guid.NewGuid();

        var calfInputModel = new CalfInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2025, 1, 1),
            Type = "Calf",
            Gender = true,
            MotherNumber = 1
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"))
            .Returns(true);

        // Act
        AnimalService.UpdateCalf(calfId, calfInputModel);

        // Assert
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateCalf(calfId, It.IsAny<Calf>()), Times.Once);
    }

    [Fact]
    public void UpdateCow_WithNonExistentRegisterNumber_ThrowsNotFoundException()
    {
        var calfId = Guid.NewGuid();

        var calfInputModel = new CalfInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2025, 1, 1),
            Type = "Calf",
            Gender = true,
            MotherNumber = 1
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"))
            .Returns(false);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.SaveCalf(calfInputModel));
        Assert.Equal($"A vaca com o numero de registro {calfInputModel.MotherNumber} nao existe.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"), Times.Once);
        MockCommandRepository.Verify(x => x.SaveCalf(It.IsAny<Calf>()), Times.Never);
    }
}
