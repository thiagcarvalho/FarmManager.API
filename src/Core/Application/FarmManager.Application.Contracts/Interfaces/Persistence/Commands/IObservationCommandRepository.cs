namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface IObservationCommandRepository
{
    int SaveObservation(Guid animalId, string observation);
    void DeleteObservation(int id);
}