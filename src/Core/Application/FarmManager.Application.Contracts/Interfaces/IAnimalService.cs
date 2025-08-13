using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces;

public interface IAnimalService
{
    Guid SaveAnimal(AnimalInputModel animalInputModel);
    void UpdateAnimal(Guid Id, AnimalInputModel animalInputModel);
    AnimalViewModel? GetAnimal(Guid Id);
    List<AnimalViewModel> GetAllAnimals();
}
