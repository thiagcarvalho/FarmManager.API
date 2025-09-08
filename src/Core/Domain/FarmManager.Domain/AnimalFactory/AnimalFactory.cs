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

    public Cow Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool isPregnant, bool hasCalf, string? name, bool isMilking)
    {
        return new Cow(id, registerNumber, weight, type, birthday, name, isPregnant, hasCalf, isMilking)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Birthday = birthday,
            IsPregnant = isPregnant,
            HasCalf = hasCalf,
            Name = name,
            IsMilking = isMilking
        };
    }

    public Calf Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool gender, int motherNumber)
    {
        return new Calf(id, registerNumber, weight, type, birthday, gender, motherNumber)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            Birthday = birthday,
            Gender = gender,
            MotherNumber = motherNumber
        };
    }

    public Bull Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, string name)
    {
        return new Bull(id, registerNumber, weight, type, birthday, name)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            Birthday = birthday,
            Name = name
        };
    }
}
