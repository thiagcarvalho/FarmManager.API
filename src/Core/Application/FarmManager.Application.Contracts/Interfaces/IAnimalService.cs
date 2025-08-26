using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces;

public interface IAnimalService
{
    Guid SaveAnimal(AnimalInputModel animalInputModel);
    Guid SaveCow(CowInputModel cowInputModel);
    void UpdateAnimal(Guid Id, AnimalInputModel animalInputModel);
    AnimalViewModel? GetAnimal(Guid Id);
    CowViewModel? GetCow(Guid Id);
    List<AnimalViewModel> GetAllAnimals();
    void DeleteAnimal(Guid Id);
}
