using FarmManager.Domain.Entities;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Interfaces.Factories;

public interface IAnimalFactory
{
    Animal Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, int loteId);
    Cow Create (Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool isPregnant, bool hasCalf, string? name, bool isMilking, int loteId);
    Calf Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, bool gender, int motherNumber, int loteId);
    Bull Create(Guid? id, int registerNumber, Arroba weight, string type, DateTime birthday, string name, int loteId);
}
