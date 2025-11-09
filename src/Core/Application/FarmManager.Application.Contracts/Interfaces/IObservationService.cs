namespace FarmManager.Application.Contracts.Interfaces;

using FarmManager.Application.Contracts.Models.ViewModels;

public interface IObservationService
{
    int AddObservation(Guid animalId, List<string> description);
    void UpdateObservation(Guid animalId, List<string> description);
    List<ObservationViewModel> GetByAnimalId(Guid animalId);
    void DeleteObservation(int observationId);
}