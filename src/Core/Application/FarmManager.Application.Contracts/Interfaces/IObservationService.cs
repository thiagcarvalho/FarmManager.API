namespace FarmManager.Application.Contracts.Interfaces;

using FarmManager.Application.Contracts.Models.ViewModels;

public interface IObservationService
{
    int AddObservation(Guid animalId, string description);
    List<ObservationViewModel> GetByAnimalId(Guid animalId);
    void DeleteObservation(int observationId);
}