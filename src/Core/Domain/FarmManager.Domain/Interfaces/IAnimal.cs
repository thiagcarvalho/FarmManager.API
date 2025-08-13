using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Interfaces;

public interface IAnimal
{
    int RegisterNumber{ get; set; }
    int Age { get; set; }
    Arroba Weight { get; set; }
    string Type { get; set; }
    DateTime Birthday { get; set; }

    void UpdateWeight(Arroba newWeight);
    void UpdateAge(int newAge);

}
