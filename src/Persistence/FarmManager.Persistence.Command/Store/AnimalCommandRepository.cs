using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Domain.Entities;
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

        MemoryStorage.Animals.Add(animalDataModel.RegisterNumber, animalDataModel);

        return animalDataModel.Id;
    }

    public void UpdateAnimal(Guid Id, Animal animal)
    {
        throw new NotImplementedException();
    }
}
