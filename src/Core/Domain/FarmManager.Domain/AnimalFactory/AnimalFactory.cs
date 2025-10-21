using FarmManager.Domain.Entities;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.AnimalFactory;

public class AnimalFactory : IAnimalFactory
{
    public Animal Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, string loteName)
    {
        return new Animal(id, registerNumber, weight, type, birthday, loteName)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            Birthday = birthday,
            LoteName = loteName
        };
    }

    public Cow Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool isPregnant, bool hasCalf, string? name, bool isMilking, string loteName)
    {
        return new Cow(id, registerNumber, weight, type, birthday, name, isPregnant, hasCalf, isMilking, loteName)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Birthday = birthday,
            LoteName = loteName,
            IsPregnant = isPregnant,
            HasCalf = hasCalf,
            Name = name,
            IsMilking = isMilking
        };
    }

    public Calf Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool gender, int motherNumber, string loteName)
    {
        return new Calf(id, registerNumber, weight, type, birthday, gender, motherNumber, loteName)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            Birthday = birthday,
            LoteName = loteName,
            Gender = gender,
            MotherNumber = motherNumber
        };
    }

    public Bull Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, string name, string loteName)
    {
        return new Bull(id, registerNumber, weight, type, birthday, name, loteName)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            LoteName = loteName,
            Birthday = birthday,
            Name = name
        };
    }
}
