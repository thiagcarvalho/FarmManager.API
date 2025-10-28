using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Interfaces;

public interface IAnimal
{
    Guid Id { get; set; }
    int RegisterNumber{ get; set; }
    Arroba Weight { get; set; }
    string Type { get; set; }
    DateTime Birthday { get; set; }

    int? LoteId { get; set; }
    void UpdateWeight(Arroba newWeight);
}
