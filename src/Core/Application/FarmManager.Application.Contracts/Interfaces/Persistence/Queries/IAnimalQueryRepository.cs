using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface IAnimalQueryRepository
{
    AnimalViewModel? GetAnimal(Guid Id);
    CowViewModel? GetCow(Guid Id);
    CalfViewModel? GetCalf(Guid Id);
    List<AnimalViewModel> GetAllAnimals();
    List<CowViewModel> GetAllCows();
    List<CalfViewModel> GetAllCalves();
    bool CowExists(int registerNumber);
}
