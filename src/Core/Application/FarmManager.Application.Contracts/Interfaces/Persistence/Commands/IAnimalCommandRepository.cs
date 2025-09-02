using FarmManager.Domain.Entities;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface IAnimalCommandRepository
{
    Guid SaveAnimal(Animal animal);
    Guid SaveCow(Cow cow);
    Guid SaveCalf(Calf calf);
    void UpdateAnimal(Guid Id, Animal animal);
    void UpdateCow(Guid Id, Cow cow);
    void DeleteAnimal(Guid Id);

}
