using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface IAnimalQueryRepository
{
    AnimalViewModel? GetAnimal(Guid Id);
    CowViewModel? GetCow(Guid Id);
    CowViewModel? GetCowByRegisterNumber(int registerNumber);
    CalfViewModel? GetCalf(Guid Id);
    CalfViewModel? GetCalfByMotherNumber(int motherNumber);
    BullViewModel? GetBull(Guid Id);
    List<AnimalViewModel> GetAllAnimals();
    List<CowViewModel> GetAllCows();
    List<CalfViewModel> GetAllCalves();
    List<BullViewModel> GetAllBulls();
    bool AnimalExistsByRegisterNumber(int registerNumber);
    bool AnimalExistsByRegisterNumberAndType(int registerNumber, string type);
    int? GetCowIdByAnimalId(Guid animalId);
    int? GetCowIdByRegisterNumber(int registerNumber);
    List<AnimalViewModel> GetAnimalsByLoteId(int loteId);
}
