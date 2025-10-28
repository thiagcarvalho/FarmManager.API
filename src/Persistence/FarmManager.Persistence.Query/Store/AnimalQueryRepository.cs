using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Persistence.Query.Store;

public class AnimalQueryRepository : IAnimalQueryRepository
{
    private readonly FarmManagerDbContext _context;
    private readonly IMapper _mapper;

    public AnimalQueryRepository(IMapper mapper, FarmManagerDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public AnimalViewModel? GetAnimal(Guid Id)
    {
        var animal = _context
             .Animals
             .FirstOrDefault(a => a.Id == Id);

        return _mapper.Map<AnimalViewModel?>(animal);
    }

    public List<AnimalViewModel> GetAllAnimals()
    {
        var animals = _context
            .Animals
            .ToList();

        return _mapper.Map<List<AnimalViewModel>>(animals);
    }

    public CowViewModel? GetCow(Guid Id)
    {
        var cow = _context.Cows
            .Include(c => c.Animal)
                .ThenInclude(a => a.Lote)
            .FirstOrDefault(c => c.AnimalId == Id);

        return _mapper.Map<CowViewModel?>(cow);
    }
    public List<CowViewModel> GetAllCows()
    {
        var cows = _context
            .Cows
            .Include(c => c.Animal)
                .ThenInclude(a => a.Lote)
            .ToList();

        return _mapper.Map<List<CowViewModel>>(cows);
    }

    public CalfViewModel? GetCalf(Guid Id)
    {
        var calf = _context.Calves
            .Include(calf => calf.Animal)
                .ThenInclude(a => a.Lote)
            .FirstOrDefault(a => a.AnimalId == Id);

        return _mapper.Map<CalfViewModel?>(calf);
    }

    public List<CalfViewModel> GetAllCalves()
    {
        var calves = _context
            .Calves
            .Include(calf => calf.Animal)
                .ThenInclude(a => a.Lote)
            .ToList();

        return _mapper.Map<List<CalfViewModel>>(calves);
    }

    public BullViewModel? GetBull(Guid Id)
    {
        var bull = _context.Bulls
            .Include(b => b.Animal)
                .ThenInclude(a => a.Lote)
            .FirstOrDefault(a => a.AnimalId == Id);

        return _mapper.Map<BullViewModel?>(bull);
    }

    public List<BullViewModel> GetAllBulls()
    {
        var bulls = _context
            .Bulls
            .Include(b => b.Animal)
                .ThenInclude(a => a.Lote)
            .ToList();

        return _mapper.Map<List<BullViewModel>>(bulls);
    }

    public bool AnimalExistsByRegisterNumber(int registerNumber)
    {
        var exists = _context
            .Animals
            .Any(a => a.RegisterNumber == registerNumber);

        return exists;
    }

    public bool AnimalExistsByRegisterNumberAndType(int registerNumber, string type)
    {
        var exists = _context
            .Animals
            .Any(a => a.RegisterNumber == registerNumber && a.Type == type);

        return exists;
    }

    public List<AnimalViewModel> GetAnimalsByLoteId(int loteId)
    {
        var animals = _context.Animals
            .Include(a => a.Lote)
            .Where(a => a.LoteId == loteId)
            .ToList();

        return _mapper.Map<List<AnimalViewModel>>(animals);
    }
}
