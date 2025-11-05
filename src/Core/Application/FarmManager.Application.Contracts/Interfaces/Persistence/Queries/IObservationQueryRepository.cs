using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface IObservationQueryRepository
{
    List<ObservationViewModel> GetByAnimalId(Guid animalId);
    ObservationViewModel? GetObservation(int id);
}