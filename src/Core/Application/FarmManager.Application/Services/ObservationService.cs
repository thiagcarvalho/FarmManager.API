using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Services;

public class ObservationService : IObservationService
{
    private readonly IObservationCommandRepository _observationsCommandRepository;
    private readonly IObservationQueryRepository _observationQueryRepository;

    public ObservationService(IObservationCommandRepository observationsQueryRepository, 
        IObservationQueryRepository observationQueryRepository)
    {
        _observationsCommandRepository = observationsQueryRepository;
        _observationQueryRepository = observationQueryRepository;
    }

    public int AddObservation(Guid animalId, List<string> description)
    {
        return _observationsCommandRepository.SaveObservation(animalId, description);
    }

    public List<ObservationViewModel> GetByAnimalId(Guid animalId) =>
        _observationQueryRepository.GetByAnimalId(animalId);

    public void DeleteObservation(int observationId) =>
        _observationsCommandRepository.DeleteObservation(observationId);

    public void UpdateObservation(Guid animalId, List<string> description)
    {
        _observationsCommandRepository.UpdateObservation(animalId, description);
    }
}