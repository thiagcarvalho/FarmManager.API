using AutoMapper;
using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Services;
using FarmManager.Domain.Interfaces.Factories;
using Moq;

namespace FarmManager.Application.Tests.Services;

public abstract class AnimalServiceTestBase
{
    protected readonly Mock<IAnimalQueryRepository> MockQueryRepository;
    protected readonly Mock<IAnimalCommandRepository> MockCommandRepository;
    protected readonly Mock<IAnimalFactory> MockAnimalFactory;
    protected readonly Mock<ILoteService> MockLoteService;
    protected readonly Mock<IMapper> MockMapper;
    protected readonly AnimalService AnimalService;

    protected AnimalServiceTestBase()
    {
        MockQueryRepository = new Mock<IAnimalQueryRepository>();
        MockCommandRepository = new Mock<IAnimalCommandRepository>();
        MockAnimalFactory = new Mock<IAnimalFactory>();
        MockLoteService = new Mock<ILoteService>();
        MockMapper = new Mock<IMapper>();
        AnimalService = new AnimalService(
            MockQueryRepository.Object,
            MockCommandRepository.Object,
            MockAnimalFactory.Object,
            MockLoteService.Object,
            MockMapper.Object);
    }
}