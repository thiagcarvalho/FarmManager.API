using FarmManager.Domain.Interfaces;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Entities;

public class Animal : IAnimal
{
    public int RegisterNumber { get; set; }
    public int Age { get; set; }
    public Arroba Weight { get; set; } = new Arroba(0);
    public string Type { get; set; } = string.Empty;

    public Animal(int registerNumber,
        int age,
        Arroba weight,
        string type)
    {
        RegisterNumber = registerNumber;
        Age = age;
        Weight = weight;
        Type = type;
    }

    public void UpdateAge(int newAge) => Age = newAge;

    public void UpdateWeight(Arroba newWeight) => Weight = newWeight;
}
