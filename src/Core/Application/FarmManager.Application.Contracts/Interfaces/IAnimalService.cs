using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces;

public interface IAnimalService
{
    Guid SaveAnimal(AnimalInputModel animalInputModel);
    Guid SaveCow(CowInputModel cowInputModel);
    Guid SaveCalf(CalfInputModel calfInputModel);
    void UpdateAnimal(Guid Id, AnimalInputModel animalInputModel);
    void UpdateCow(Guid Id, CowInputModel cowInputModel);
    void UpdateCalf(Guid Id, CalfInputModel calfInputModel);
    AnimalViewModel? GetAnimal(Guid Id);
    CowViewModel? GetCow(Guid Id);
    CalfViewModel? GetCalf(Guid Id);
    List<AnimalViewModel> GetAllAnimals();
    List<CowViewModel> GetAllCows();
    void DeleteAnimal(Guid Id);
}
