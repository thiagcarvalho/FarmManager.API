using FarmManager.Domain.Interfaces;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Entities;

public class Animal : IAnimal
{
    public Guid Id { get; set; }
    public int RegisterNumber { get; set; }
    public Arroba Weight { get; set; } = new Arroba(0);
    public string Type { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }

    public int Age => CalculateAge();

    internal Animal(Guid? id,
        int registerNumber,
        Arroba weight,
        string type,
        DateTime birthday)
    {
        Id = id ?? Guid.NewGuid();
        RegisterNumber = registerNumber;
        Weight = weight;
        Type = type;
        Birthday = birthday;
    }

    private int CalculateAge()
    {
        var today = DateTime.Today;
        int age = today.Year - Birthday.Year;
        if (Birthday.Date > today.AddYears(-age)) age--;
        return age;
    }

    public void UpdateWeight(Arroba newWeight) => Weight = newWeight;
}
