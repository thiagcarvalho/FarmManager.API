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
        int id = FindAnimalKeyInDictionary(Id);
        var animalDataModel = _mapper.Map<AnimalDataModel>(animal);

        MemoryStorage.Animals[animalDataModel.RegisterNumber] = animalDataModel;
    }

    public void DeleteAnimal(Guid Id)
    {
        int id = FindAnimalKeyInDictionary(Id);
        MemoryStorage.Animals.Remove(id);
    }

    public Guid SaveCow(Cow cow)
    {
        var cowDataModel = _mapper.Map<CowDataModel>(cow);

        cowDataModel.CreatedAt = DateTime.UtcNow;
        cowDataModel.CreatedBy = "System";

        MemoryStorage.Animals.Add(DictLen(), cowDataModel);

        return cowDataModel.Id;
    }

    public void UpdateCow(Guid Id, Cow cow)
    {
        int id = FindAnimalKeyInDictionary(Id);
        var cowDataModel = _mapper.Map<CowDataModel>(cow);

        MemoryStorage.Animals[id] = cowDataModel;
    }

    public Guid SaveCalf(Calf calf)
    {
        var calfDataModel = _mapper.Map<CalfDataModel>(calf);

        calfDataModel.CreatedAt = DateTime.UtcNow;
        calfDataModel.CreatedBy = "System";

        MemoryStorage.Animals.Add(DictLen(), calfDataModel);

        return calfDataModel.Id;
    }

    public void UpdateCalf(Guid Id, Calf calf)
    {
        int id = FindAnimalKeyInDictionary(Id);
        var calfDataModel = _mapper.Map<CalfDataModel>(calf);

        MemoryStorage.Animals[id] = calfDataModel;
    }

    private int FindAnimalKeyInDictionary(Guid animalId)
    {
        var kvp = MemoryStorage.Animals.FirstOrDefault(kvp => kvp.Value.Id == animalId);

        if (kvp.Value == null)
        {
            throw new KeyNotFoundException($"Animal with Id {animalId} not found in dictionary.");
        }

        return kvp.Key;
    }

    private int DictLen()
    {
        return MemoryStorage.Animals.Count + 1;
    }

}
