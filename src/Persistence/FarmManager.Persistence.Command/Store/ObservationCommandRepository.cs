using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Domain.Entities;
using FarmManager.Persistence.DataModels.Store;
using FarmManager.Persistence.EF.Context;

namespace FarmManager.Persistence.Command.Store;

public class ObservationCommandRepository : IObservationCommandRepository
{
    private readonly FarmManagerDbContext _context;

    public ObservationCommandRepository(FarmManagerDbContext context)
    {
        _context = context;
    }

    public int SaveObservation(Guid animalId, List<string> observation)
    {
        var observations = observation.Select(desc => new ObservationDataModel
        {
            AnimalId = animalId,
            Description = desc,
            DataAdd = DateTime.UtcNow
        }).ToList();

        _context.Observations.AddRange(observations);
        _context.SaveChanges();

        return observations.First().Id;
    }

    public void DeleteObservation(int id)
    {
        var obs = _context.Observations.Find(id);
        if (obs != null)
        {
            _context.Observations.Remove(obs);
            _context.SaveChanges();
        }
    }

    public int UpdateObservation(Guid animalId, List<string> observation)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            if (observation == null || observation.Count == 0)
            {
                DeleteAllObservation(animalId);
                transaction.Commit();
                return 0;
            }

            var existingObservations = GetExistingObservations(animalId);

            DeleteRemovedObservations(existingObservations, observation);

            var newObservations = AddNewObservations(animalId, observation, existingObservations);

            _context.SaveChanges();
            transaction.Commit();

            return GetFirstObservationId(newObservations, existingObservations);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private List<ObservationDataModel> GetExistingObservations(Guid animalId)
    {
        return _context.Observations
            .Where(o => o.AnimalId == animalId)
            .ToList();
    }

    private void DeleteRemovedObservations(
        List<ObservationDataModel> existingObservations,
        List<string> sentObservations)
    {
        var observationsToDelete = existingObservations
            .Where(existing => !sentObservations.Contains(existing.Description))
            .ToList();

        if (observationsToDelete.Any())
        {
            _context.Observations.RemoveRange(observationsToDelete);
        }
    }

    private List<ObservationDataModel> AddNewObservations(
        Guid animalId,
        List<string> sentObservations,
        List<ObservationDataModel> existingObservations)
    {
        var existingDescriptions = existingObservations
            .Select(o => o.Description)
            .ToList();

        var newObservations = sentObservations
            .Where(desc => !existingDescriptions.Contains(desc))
            .Select(desc => new ObservationDataModel
            {
                AnimalId = animalId,
                Description = desc,
                DataAdd = DateTime.UtcNow
            })
            .ToList();

        if (newObservations.Any())
        {
            _context.Observations.AddRange(newObservations);
        }

        return newObservations;
    }

    private int GetFirstObservationId(
        List<ObservationDataModel> newObservations,
        List<ObservationDataModel> existingObservations)
    {
        return newObservations.Any()
            ? newObservations.First().Id
            : existingObservations.FirstOrDefault()?.Id ?? 0;
    }

    private void DeleteAllObservation(Guid animalId)
    {
        var obs = _context.Observations.Where(o => o.AnimalId == animalId).ToList();
        if (obs.Any())
        {
            _context.Observations.RemoveRange(obs);
            _context.SaveChanges();
        }
    }
}