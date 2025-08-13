using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalQueryRepository _animalQueryRepository;

    public AnimalService(IAnimalQueryRepository animalQueryRepository)
    {
        _animalQueryRepository = animalQueryRepository;
    }
    public List<AnimalViewModel> GetAllAnimals()
    {
        return _animalQueryRepository.GetAllAnimals();
    }

    public AnimalViewModel? GetAnimal(Guid Id)
    {
        return _animalQueryRepository.GetAnimal(Id);
    }

    public Guid SaveAnimal(AnimalInputModel animalInputModel)
    {
        throw new NotImplementedException();
    }

    public void UpdateAnimal(Guid Id, AnimalInputModel animalInputModel)
    {
        throw new NotImplementedException();
    }
}
