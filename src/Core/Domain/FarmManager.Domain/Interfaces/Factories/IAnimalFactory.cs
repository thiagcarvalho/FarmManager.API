using FarmManager.Domain.Entities;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Interfaces.Factories;

public interface IAnimalFactory
{
    Animal Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, string loteName);
    Cow Create (Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool isPregnant, bool hasCalf, string? name, bool isMilking, string loteName);
    Calf Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool gender, int motherNumber, string loteName);
    Bull Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, string name, string loteName);
}
