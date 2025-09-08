using FarmManager.Domain.Interfaces;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Entities;

public class Bull: Animal, IBull
{
    public string Name { get; set; }

    public Bull(Guid? id,
        int registerNumber,
        Arroba weight,
        string type,
        DateTime birthday,
        string name) : base(id, registerNumber, weight, type, birthday)
    {
        Name = name;
    }
}
