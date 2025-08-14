using FarmManager.Domain.Entities;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.AnimalFactory;

public class AnimalFactory : IAnimalFactory
{
    public Animal Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday)
    {
        return new Animal(id, registerNumber, weight, type, birthday)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            Birthday = birthday
        };
    }
}
