using FarmManager.Domain.Entities;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface IAnimalCommandRepository
{
    Guid SaveAnimal(Animal animal);
    Guid SaveCow(Cow cow);
    void UpdateAnimal(Guid Id, Animal animal);
    void DeleteAnimal(Guid Id);

}
