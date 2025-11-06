using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
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

    public int SaveObservation(Guid animalId, string observation)
    {
        var obs = new ObservationDataModel
        {
            AnimalId = animalId,
            Description = observation,
            DataAdd = DateTime.UtcNow
        };

        _context.Observations.Add(obs);
        _context.SaveChanges();

        return obs.Id;
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
}