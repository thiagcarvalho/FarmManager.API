namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface IObservationCommandRepository
{
    int SaveObservation(string observation);
    void DeleteObservation(int id);
}