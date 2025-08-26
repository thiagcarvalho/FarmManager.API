using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Domain.Entities;
using FarmManager.Domain.Interfaces;
using FarmManager.Persistence.DataModels;
using FarmManager.Persistence.DataModels.Store;

namespace FarmManager.Persistence.Command.Store;

public class AnimalCommandRepository : IAnimalCommandRepository
{
    private readonly IMapper _mapper;

    public AnimalCommandRepository(IMapper mapper)
    {
        _mapper = mapper;
    }


    public Guid SaveAnimal(Animal animal)
    {
        var animalDataModel = _mapper.Map<AnimalDataModel>(animal);

        animalDataModel.CreatedAt = DateTime.UtcNow;
        animalDataModel.CreatedBy = "System";

        MemoryStorage.Animals.Add(DictLen(), animalDataModel);

        return animalDataModel.Id;
    }

    public void UpdateAnimal(Guid Id, Animal animal)
    {
        var animalDataModel = _mapper.Map<AnimalDataModel>(animal);

        MemoryStorage.Animals.TryGetValue(animalDataModel.RegisterNumber, out var existingAnimal);

        if (existingAnimal == null)
        {
            throw new KeyNotFoundException($"Animal with RegisterNumber {animalDataModel.RegisterNumber} not found.");
        }

        MemoryStorage.Animals[animalDataModel.RegisterNumber] = animalDataModel;
    }

    public void DeleteAnimal(Guid Id)
    {
        var animalDataModel = MemoryStorage.Animals.Values.FirstOrDefault(a => a.Id == Id);

        if (animalDataModel == null)
        {
            throw new KeyNotFoundException($"Animal with Id {Id} not found.");
        }

        MemoryStorage.Animals.Remove(animalDataModel.RegisterNumber);
    }

    public Guid SaveCow(Cow cow)
    {
        var cowDataModel = _mapper.Map<CowDataModel>(cow);

        cowDataModel.CreatedAt = DateTime.UtcNow;
        cowDataModel.CreatedBy = "System";

        MemoryStorage.Animals.Add(DictLen(), cowDataModel);

        return cowDataModel.Id;
    }

    private int DictLen()
    {
        return MemoryStorage.Animals.Count + 1;
    }
}
