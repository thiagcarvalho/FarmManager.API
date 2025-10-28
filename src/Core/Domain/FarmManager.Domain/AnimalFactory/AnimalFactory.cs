using FarmManager.Domain.Entities;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.AnimalFactory;

public class AnimalFactory : IAnimalFactory
{
    public Animal Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, int loteId)
    {
        return new Animal(id, registerNumber, weight, type, birthday, loteId)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            Birthday = birthday,
            LoteId = loteId
        };
    }

    public Cow Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool isPregnant, bool hasCalf, string? name, bool isMilking, int loteId)
    {
        return new Cow(id, registerNumber, weight, type, birthday, name, isPregnant, hasCalf, isMilking, loteId)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Birthday = birthday,
            LoteId = loteId,
            IsPregnant = isPregnant,
            HasCalf = hasCalf,
            Name = name,
            IsMilking = isMilking
        };
    }

    public Calf Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool gender, int motherNumber, int loteId)
    {
        return new Calf(id, registerNumber, weight, type, birthday, gender, motherNumber, loteId)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            Birthday = birthday,
            LoteId = loteId,
            Gender = gender,
            MotherNumber = motherNumber
        };
    }

    public Bull Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, string name, int loteId)
    {
        return new Bull(id, registerNumber, weight, type, birthday, name, loteId)
        {
            Id = id ?? Guid.NewGuid(),
            RegisterNumber = registerNumber,
            Weight = weight,
            Type = type,
            LoteId = loteId,
            Birthday = birthday,
            Name = name
        };
    }
}
