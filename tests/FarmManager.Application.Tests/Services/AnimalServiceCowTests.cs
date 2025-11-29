using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using Moq;

namespace FarmManager.Application.Tests.Services;

public class AnimalServiceCowTests : AnimalServiceTestBase
{
    [Fact]
    public void GetCow_WithValidId_ReturnsCowViewModel()
    {
        //Arrange
        var expectedCow = new CowViewModel
        {
            Id = Guid.NewGuid(),
            RegisterNumber = 1,
            Age = 5,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow",
            Name = "Bessie",
            IsPregnant = true,
            HasCalf = false,
            IsMilking = true
        };

        MockQueryRepository
            .Setup(repo => repo.GetCow(It.IsAny<Guid>()))
            .Returns(expectedCow);

        //Act

        var result = AnimalService.GetCow(expectedCow.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCow.Id, result.Id);
        Assert.Equal(expectedCow.RegisterNumber, result.RegisterNumber);
        Assert.Equal(expectedCow.Age, result.Age);
        Assert.Equal(expectedCow.Weight, result.Weight);
        Assert.Equal(expectedCow.Birthday, result.Birthday);
        Assert.Equal(expectedCow.Type, result.Type);
        Assert.Equal(expectedCow.Name, result.Name);
        Assert.Equal(expectedCow.IsPregnant, result.IsPregnant);
        Assert.Equal(expectedCow.HasCalf, result.HasCalf);
        Assert.Equal(expectedCow.IsMilking, result.IsMilking);

        MockQueryRepository.Verify(x => x.GetCow(expectedCow.Id), Times.Once);
    }

    [Fact]
    public void GetCow_WithInvalidId_ThrowsNotFoundException()
    {
        // Arrange
        var cowId = Guid.NewGuid();

        MockQueryRepository
            .Setup(x => x.GetCow(cowId))
            .Returns((CowViewModel?)null);

        // Act & Assert
        var exception = Assert.Throws<NotFoundException>(() => AnimalService.GetCow(cowId));

        Assert.Equal($"Entity \"Cow\" ({cowId}) was not found.", exception.Message);
        MockQueryRepository.Verify(x => x.GetCow(cowId), Times.Once);
    }

    [Fact]
    public void GetAllCows_ReturnsListOfCowViewModels()
    {
        // Arrange
        var expectedCows = new List<CowViewModel>
        {
            new CowViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 1,
                Age = 5,
                Weight = 150.5m,
                Birthday = new DateTime(2020, 1, 1),
                Type = "Cow",
                Name = "Bessie",
                IsPregnant = true,
                HasCalf = false,
                IsMilking = true
            },
            new CowViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 2,
                Age = 3,
                Weight = 130.0m,
                Birthday = new DateTime(2022, 1, 1),
                Type = "Cow",
                Name = "Daisy",
                IsPregnant = false,
                HasCalf = true,
                IsMilking = false
            },
            new CowViewModel
            {
                Id = Guid.NewGuid(),
                RegisterNumber = 3,
                Age = 4,
                Weight = 140.0m,
                Birthday = new DateTime(2021, 1, 1),
                Type = "Cow",
                Name = "Molly",
                IsPregnant = false,
                HasCalf = false,
                IsMilking = true
            }
        };

        MockQueryRepository
            .Setup(repo => repo.GetAllCows())
            .Returns(expectedCows);

        // Act
        var result = AnimalService.GetAllCows();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCows.Count, result.Count);
        Assert.Equal(expectedCows, result);
        MockQueryRepository.Verify(x => x.GetAllCows(), Times.Once);
    }

    [Fact]
    public void GettAllCows_ReturnsEmptyList()
    {
        // Arrange
        var expectedCow = new List<CowViewModel>();
        MockQueryRepository
            .Setup(repo => repo.GetAllCows())
            .Returns(expectedCow);

        // Act
        var result = AnimalService.GetAllCows();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        MockQueryRepository.Verify(x => x.GetAllCows(), Times.Once);
    }

    [Fact]
    public void SaveCow_WithValidInput_ReturnsNewCowId()
    {
        // Arrange
        var newCowId = Guid.NewGuid();

        var cowInputModel = new CowInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow",
            Name = "Bessie",
            IsPregnant = true,
            HasCalf = false,
            IsMilking = true
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(cowInputModel.RegisterNumber))
            .Returns(false);

        MockCommandRepository
            .Setup(repo => repo.SaveCow(It.IsAny<Domain.Entities.Cow>()))
            .Returns(newCowId);

        // Act
        var result = AnimalService.SaveCow(cowInputModel);

        // Assert
        Assert.Equal(newCowId, result);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(cowInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.SaveCow(It.IsAny<Domain.Entities.Cow>()), Times.Once);
    }

    [Fact]
    public void SaveCow_WithDuplicateRegisterNumber_ThrowsValidationException()
    {
        // Arrange
        var cowInputModel = new CowInputModel
        {
            RegisterNumber = 1,
            Weight = 150.5m,
            Birthday = new DateTime(2020, 1, 1),
            Type = "Cow",
            Name = "Bessie",
            IsPregnant = true,
            HasCalf = false,
            IsMilking = true
        };

        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumber(cowInputModel.RegisterNumber))
            .Returns(true);

        // Act & Assert
        var exception = Assert.Throws<DuplicateResourceException>(() => AnimalService.SaveCow(cowInputModel));

        Assert.Equal($"Animal with identifier '{cowInputModel.RegisterNumber}' already exists.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumber(cowInputModel.RegisterNumber), Times.Once);
        MockCommandRepository.Verify(x => x.SaveCow(It.IsAny<Domain.Entities.Cow>()), Times.Never);
    }

    [Fact]
    public void UpdateCow_WithValidInput_CallsUpdateCow()
    {
        // Arrange
        var cowId = Guid.NewGuid();
        var cowInputModel = new CowInputModel
        {
            Id = cowId,
            RegisterNumber = 2,
            Weight = 130.0m,
            Birthday = new DateTime(2022, 1, 1),
            Type = "Cow",
            Name = "Daisy",
            IsPregnant = false,
            HasCalf = true,
            IsMilking = false
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumberExcludingId(cowInputModel.RegisterNumber, cowId))
            .Returns(false);

        // Act
        AnimalService.UpdateCow(cowId, cowInputModel);

        // Assert
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumberExcludingId(cowInputModel.RegisterNumber, cowId), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateCow(cowId, It.IsAny<Domain.Entities.Cow>()), Times.Once);
    }

    [Fact]
    public void UpdateCow_WithDuplicateRegisterNumber_ThrowsDuplicateResourceException()
    {
        // Arrange
        var cowId = Guid.NewGuid();
        var cowInputModel = new CowInputModel
        {
            Id = cowId,
            RegisterNumber = 2,
            Weight = 130.0m,
            Birthday = new DateTime(2022, 1, 1),
            Type = "Cow",
            Name = "Daisy",
            IsPregnant = false,
            HasCalf = true,
            IsMilking = false
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumberExcludingId(cowInputModel.RegisterNumber, cowId))
            .Returns(true);

        // Act & Assert
        var exception = Assert.Throws<DuplicateResourceException>(() => AnimalService.UpdateCow(cowId, cowInputModel));
        Assert.Equal($"Animal with identifier '{cowInputModel.RegisterNumber}' already exists.", exception.Message);
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumberExcludingId(cowInputModel.RegisterNumber, cowId), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateCow(cowId, It.IsAny<Domain.Entities.Cow>()), Times.Never);
    }

    [Fact]
    public void UpdateCow_WithSameRegisterNumber_CallsUpdateCow()
    {
        // Arrange - Valida que pode atualizar mantendo o mesmo RegisterNumber
        var cowId = Guid.NewGuid();
        var cowInputModel = new CowInputModel
        {
            Id = cowId,
            RegisterNumber = 5,
            Weight = 160.0m,
            Birthday = new DateTime(2021, 6, 15),
            Type = "Cow",
            Name = "Bessie",
            IsPregnant = true,
            HasCalf = false,
            IsMilking = true
        };
        MockQueryRepository
            .Setup(repo => repo.AnimalExistsByRegisterNumberExcludingId(cowInputModel.RegisterNumber, cowId))
            .Returns(false);

        // Act
        AnimalService.UpdateCow(cowId, cowInputModel);

        // Assert
        MockQueryRepository.Verify(x => x.AnimalExistsByRegisterNumberExcludingId(cowInputModel.RegisterNumber, cowId), Times.Once);
        MockCommandRepository.Verify(x => x.UpdateCow(cowId, It.IsAny<Domain.Entities.Cow>()), Times.Once);
    }
}