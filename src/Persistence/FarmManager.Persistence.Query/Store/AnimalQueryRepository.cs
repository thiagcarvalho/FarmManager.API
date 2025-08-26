using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.DataModels;
using FarmManager.Persistence.DataModels.Store;

namespace FarmManager.Persistence.Query.Store;

public class AnimalQueryRepository : IAnimalQueryRepository
{
    private readonly IMapper _mapper;

    public AnimalQueryRepository(IMapper mapper)
    {
        _mapper = mapper;
    }

    public AnimalViewModel? GetAnimal(Guid Id)
    {
        var animal = MemoryStorage
            .Animals
            .Values
            .FirstOrDefault(a => a.Id == Id);

        return _mapper.Map<AnimalViewModel?>(animal);
    }

    public List<AnimalViewModel> GetAllAnimals()
    {
        var animals = MemoryStorage
            .Animals
            .Values
            .ToList();

        return _mapper.Map<List<AnimalViewModel>>(animals);
    }

    public CowViewModel? GetCow(Guid Id)
    {
        var cow = MemoryStorage
             .Animals
             .Values
             .FirstOrDefault(a => a.Id == Id);

        return _mapper.Map<CowViewModel?>(cow);
    }

    public List<CowViewModel> GetAllCows()
    {
        var cows = MemoryStorage
            .Animals
            .Values
            .Where(a => a.Type == "Cow")
            .OfType<CowDataModel>()
            .ToList();

        return _mapper.Map<List<CowViewModel>>(cows);
    }

}
