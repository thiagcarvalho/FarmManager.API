namespace FarmManager.Domain.Interfaces;

public interface ICalf : IAnimal
{
    bool Gender { get; set; }
    int MotherNumber { get; set; }
}
