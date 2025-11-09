namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface IObservationCommandRepository
{
    int SaveObservation(Guid animalId, List<string> observation);
    void DeleteObservation(int id);
    int UpdateObservation(Guid animalId, List<string> observation);
}