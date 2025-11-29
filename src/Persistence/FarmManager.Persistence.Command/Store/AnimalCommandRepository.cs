using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Domain.Entities;
using FarmManager.Persistence.DataModels;
using FarmManager.Persistence.DataModels.Store;
using FarmManager.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;

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
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var animal = _context.Animals.Find(Id);
            if (animal == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(animal.Type) &&
                animal.Type.Equals("Calf", StringComparison.OrdinalIgnoreCase))
            {
                var calf = _context.Calves
                    .AsNoTracking()
                    .FirstOrDefault(c => c.AnimalId == Id);

                if (calf != null)
                {
                    var motherCow = _context.Cows
                        .Include(c => c.Animal)
                        .FirstOrDefault(c => c.Animal != null && c.Animal.RegisterNumber == calf.MotherNumber);

                    if (motherCow != null && motherCow.HasCalf)
                    {
                        motherCow.HasCalf = false;
                        motherCow.UpdatedAt = DateTime.UtcNow;
                        motherCow.UpdatedBy = "System";
                    }
                }
            }

            _context.Animals.Remove(animal);
            _context.SaveChanges();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
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
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var existingCow = _context.Cows
                .Include(c => c.Animal)
                .FirstOrDefault(c => c.AnimalId == Id);

            if (existingCow != null)
            {
                var oldRegisterNumber = existingCow.Animal.RegisterNumber;
                var newRegisterNumber = cow.RegisterNumber;

                UpdateAnimal(Id, cow);

                _mapper.Map(cow, existingCow);
                existingCow.UpdatedAt = DateTime.UtcNow;
                existingCow.UpdatedBy = "System";
                _context.SaveChanges();

                if (oldRegisterNumber != newRegisterNumber)
                {
                    var calves = _context.Calves
                        .Include(c => c.Animal)
                        .Where(c => c.MotherNumber == oldRegisterNumber)
                        .ToList();

                    foreach (var calf in calves)
                    {
                        calf.MotherNumber = newRegisterNumber;
                        calf.Animal.RegisterNumber = newRegisterNumber;
                        calf.UpdatedAt = DateTime.UtcNow;
                        calf.UpdatedBy = "System";
                    }

                    if (calves.Count > 0)
                    {
                        _context.SaveChanges();
                    }
                }
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
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

            var motherCow = _context.Cows
                .Include(c => c.Animal)
                .FirstOrDefault(c => c.Animal != null && c.Animal.RegisterNumber == calf.MotherNumber);

            if (motherCow != null)
            {
                motherCow.HasCalf = true;
                motherCow.UpdatedAt = DateTime.UtcNow;
                motherCow.UpdatedBy = "System";
                _context.SaveChanges();
            }

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
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var existingCalf = _context.Calves.FirstOrDefault(c => c.AnimalId == Id);
            if (existingCalf != null)
            {
                UpdateAnimal(Id, calf);

                _mapper.Map(calf, existingCalf);
                existingCalf.UpdatedAt = DateTime.UtcNow;
                existingCalf.UpdatedBy = "System";
                _context.SaveChanges();
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
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
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var existingBull = _context.Bulls.FirstOrDefault(b => b.AnimalId == Id);
            if (existingBull != null)
            {
                UpdateAnimal(Id, bull);

                _mapper.Map(bull, existingBull);
                existingBull.UpdatedAt = DateTime.UtcNow;
                existingBull.UpdatedBy = "System";
                _context.SaveChanges();
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
