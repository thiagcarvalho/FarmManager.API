using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Persistence.Query.Store;

public class ObservationQueryRepository : IObservationQueryRepository
{
    private readonly FarmManagerDbContext _context;

    public ObservationQueryRepository(FarmManagerDbContext context)
    {
        _context = context;
    }

    public List<ObservationViewModel> GetByAnimalId(Guid animalId)
    {
        return _context.Observations
            .AsNoTracking()
            .Where(o => o.AnimalId == animalId)
            .OrderByDescending(o => o.DataAdd)
            .Select(o => new ObservationViewModel
            {
                Description = o.Description,
                DataAdd = o.DataAdd
            })
            .ToList();
    }

    public ObservationViewModel? GetObservation(int id)
    {
        return _context.Observations
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new ObservationViewModel
            {
                Description = o.Description,
                DataAdd = o.DataAdd
            })
            .FirstOrDefault();
    }
}
