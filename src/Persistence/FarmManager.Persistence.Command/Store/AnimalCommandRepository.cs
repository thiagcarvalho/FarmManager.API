using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Domain.Entities;
using FarmManager.Persistence.DataModels;
using FarmManager.Persistence.DataModels.Store;
using FarmManager.Persistence.EF.Context;

namespace FarmManager.Persistence.Command.Store;

public class AnimalCommandRepository : IAnimalCommandRepository
{
    private readonly IMapper _mapper;
    private readonly FarmManagerDbContext _context;

    public AnimalCommandRepository(IMapper mapper, FarmManagerDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public Guid SaveAnimal(Animal animal)
    {
        var animalDataModel = _mapper.Map<AnimalDataModel>(animal);

        animalDataModel.CreatedAt = DateTime.UtcNow;
        animalDataModel.CreatedBy = "System";

        _context.Animals.Add(animalDataModel);
        _context.SaveChanges();

        return animalDataModel.Id;
    }

    public void UpdateAnimal(Guid Id, Animal animal)
    {
        var existingAnimal = _context.Animals.Find(Id);
        if (existingAnimal != null)
        {
            _mapper.Map(animal, existingAnimal);
            existingAnimal.UpdatedAt = DateTime.UtcNow;
            existingAnimal.UpdatedBy = "System";
            _context.SaveChanges();
        }
    }

    public void DeleteAnimal(Guid Id)
    {
        var animal = _context.Animals.Find(Id);
        if (animal != null)
        {
            _context.Animals.Remove(animal);
            _context.SaveChanges();
        }
    }

    public Guid SaveCow(Cow cow)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var animalId = SaveAnimal(cow);

            _context.ChangeTracker.Clear();

            var cowDataModel = _mapper.Map<CowDataModel>(cow);

            _context.Cows.Add(cowDataModel);
            _context.SaveChanges();

            transaction.Commit();
            return cowDataModel.AnimalId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void UpdateCow(Guid Id, Cow cow)
    {
        var existingCow = _context.Cows.FirstOrDefault(c => c.AnimalId == Id);
        if (existingCow != null)
        {
            _mapper.Map(cow, existingCow);
            existingCow.UpdatedAt = DateTime.UtcNow;
            existingCow.UpdatedBy = "System";
            _context.SaveChanges();
        }
    }

    public Guid SaveCalf(Calf calf)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var animalId = SaveAnimal(calf);

            var calfDataModel = _mapper.Map<CalfDataModel>(calf);
            calfDataModel.AnimalId = animalId;
            calfDataModel.CreatedAt = DateTime.UtcNow;
            calfDataModel.CreatedBy = "System";

            _context.Calves.Add(calfDataModel);
            _context.SaveChanges();

            transaction.Commit();
            return animalId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void UpdateCalf(Guid Id, Calf calf)
    {
        var existingCalf = _context.Calves.FirstOrDefault(calf => calf.AnimalId == Id);
        if (existingCalf != null)
        {
            _mapper.Map(calf, existingCalf);
            existingCalf.UpdatedAt = DateTime.UtcNow;
            existingCalf.UpdatedBy = "System";
            _context.SaveChanges();
        }
    }

    public Guid SaveBull(Bull bull)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var animalId = SaveAnimal(bull);

            var bullDataModel = _mapper.Map<BullDataModel>(bull);
            bullDataModel.AnimalId = animalId;
            bullDataModel.CreatedAt = DateTime.UtcNow;
            bullDataModel.CreatedBy = "System";

            _context.Bulls.Add(bullDataModel);
            _context.SaveChanges();

            transaction.Commit();
            return animalId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void UpdateBull(Guid Id, Bull bull)
    {
        var existingBull = _context.Bulls.FirstOrDefault(b => b.AnimalId == Id);
        if (existingBull != null)
        {
            _mapper.Map(bull, existingBull);
            existingBull.UpdatedAt = DateTime.UtcNow;
            existingBull.UpdatedBy = "System";
            _context.SaveChanges();
        }
    }
}
