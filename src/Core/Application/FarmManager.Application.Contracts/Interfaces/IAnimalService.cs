using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces;

public interface IAnimalService
{
    Guid SaveAnimal(AnimalInputModel animalInputModel);
    Guid SaveCow(CowInputModel cowInputModel);
    Guid SaveCalf(CalfInputModel calfInputModel);
    Guid SaveBull(BullInputModel bullInputModel);
    void UpdateAnimal(Guid Id, AnimalInputModel animalInputModel);
    void UpdateCow(Guid Id, CowInputModel cowInputModel);
    void UpdateCalf(Guid Id, CalfInputModel calfInputModel);
    void UpdateBull(Guid Id, BullInputModel bullInputModel);
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
    void DeleteAnimal(Guid Id);
}
