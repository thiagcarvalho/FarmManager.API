using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface IAnimalQueryRepository
{
    AnimalViewModel? GetAnimal(Guid Id);
    List<AnimalViewModel> GetAllAnimals();
}
