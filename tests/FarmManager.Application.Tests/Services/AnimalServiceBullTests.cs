using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using FarmManager.Domain.Entities;
using Moq;

namespace FarmManager.Application.Tests.Services;

public class AnimalServiceBullTests : AnimalServiceTestBase
{
    [Fact]
    public void GetBull_WithValidId_ReturnsBullViewModel()
    {
        //Arrange
        var expectedBull = new BullViewModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 2,
            Age = 4,
            Weight = 200.0m,
            Birthday = new DateTime(2019, 6, 15),
            Type = "Bull",
            Name = "Maximus"
        };
        MockQueryRepository
            .Setup(repo => repo.GetBull(It.IsAny<Guid>()))
            .Returns(expectedBull);

        //Act
        var result = AnimalService.GetBull(expectedBull.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedBull.Id, result.Id);
        Assert.Equal(expectedBull.RegisterNumber, result.RegisterNumber);
        Assert.Equal(expectedBull.Age, result.Age);
        Assert.Equal(expectedBull.Weight, result.Weight);
        Assert.Equal(expectedBull.Birthday, result.Birthday);
        Assert.Equal(expectedBull.Type, result.Type);
        Assert.Equal(expectedBull.Name, result.Name);

        MockQueryRepository.Verify(x => x.GetBull(expectedBull.Id), Times.Once);
    }

    [Fact]
    public void GetBull_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var bullId = Guid.NewGuid();
        MockQueryRepository
            .Setup(x => x.GetBull(bullId))
            .Returns((BullViewModel?)null);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.GetBull(bullId));
        Assert.Equal($"Entity \"Bull\" ({bullId}) was not found.", exception.Message);
        MockQueryRepository.Verify(x => x.GetBull(bullId), Times.Once);
    }

    [Fact]
    public void GetAllBulls_ReturnsListOfBullsViewModels()
    {
        // Arrange
        var expectedBull = new List<BullViewModel>
        {
            new BullViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 2,
                Age = 4,
                Weight = 200.0m,
                Birthday = new DateTime(2019, 6, 15),
                Type = "Bull",
                Name = "Maximus"
            },
            new BullViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 3,
                Age = 6,
                Weight = 250.0m,
                Birthday = new DateTime(2017, 3, 10),
                Type = "Bull",
                Name = "Thunder"
            },
            new BullViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 4,
                Age = 5,
                Weight = 220.0m,
                Birthday = new DateTime(2018, 8, 20),
                Type = "Bull",
                Name = "Brutus"
            }
        };

        MockQueryRepository
            .Setup(repo => repo.GetAllBulls())
            .Returns(expectedBull);

        // Act
        var result = AnimalService.GetAllBulls();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedBull.Count, result.Count);
        Assert.Equal(expectedBull, result);
        MockQueryRepository.Verify(x => x.GetAllBulls(), Times.Once);
    }

    [Fact]
    public void GettAllBulls_ReturnsEmptyList()
    {
        // Arrange
        var expectedBull = new List<BullViewModel>();
        MockQueryRepository
            .Setup(repo => repo.GetAllBulls())
            .Returns(expectedBull);

        // Act
        var result = AnimalService.GetAllBulls();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        MockQueryRepository.Verify(x => x.GetAllBulls(), Times.Once);
    }

    [Fact]
    public void SaveBull_WithValidInput_ReturnsNewBullId()
    {
        // Arrange
        var newBullId = Guid.NewGuid();

        var bullInputModel = new BullInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Bull",
            Name = "Ferdinando",
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber))
            .Returns(false);

        MockCommandRepository
            .Setup(repo => repo.SaveBull(It.IsAny<Bull>()))
            .Returns(newBullId);

        // Act
        var result = AnimalService.SaveBull(bullInputModel);

        // Assert
        Assert.Equal(newBullId, result);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.SaveBull(It.IsAny<Bull>()), Times.Once);
    }

    [Fact]
    public void SaveBull_WithDuplicateRegisterNumber_ThrowsValidationException()
    {
        // Arrange
        var bullInputModel = new BullInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2019, 6, 15),
            Type = "Bull",
            Name = "Boi Bandido"
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber))
            .Returns(true);

        // Act & Assert
        var exception = Assert.Throws<DuplicateResourceException>(() => AnimalService.SaveBull(bullInputModel));
        Assert.Equal($"Animal with identifier '{bullInputModel.RegisterNumber}' already exists.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.SaveBull(It.IsAny<Bull>()), Times.Never);
    }

    [Fact]
    public void UpdateBull_WithValidInput_CallsUpdateBull()
    {
        // Arrange
        var bullId = Guid.NewGuid();
        var bullInputModel = new BullInputModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 2,
            Weight = 130.0m,
            Birthday = new DateTime(2022, 1, 1),
            Type = "Bull",
            Name = "Thunder"
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber))
            .Returns(true);

        // Act
        AnimalService.UpdateBull(bullId, bullInputModel);

        // Assert
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateBull(bullId, It.IsAny<Bull>()), Times.Once);
    }

    [Fact]
    public void UpdateBull_WithNonExistentRegisterNumber_ThrowsNotFoundException()
    {
        // Arrange
        var bullId = Guid.NewGuid();
        var bullInputModel = new BullInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Bull",
            Name = "Ferdinando",
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber))
            .Returns(false);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.UpdateBull(bullId, bullInputModel));
        Assert.Equal($"The Bull with register number {bullInputModel.RegisterNumber} does not exist.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(bullInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateBull(bullId, It.IsAny<Bull>()), Times.Never);
    }
}
