using AutoMapper;
using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using FarmManager.Domain.Entities;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Application.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalQueryRepository _animalQueryRepository;
    private readonly IAnimalCommandRepository _animalCommandRepository;
    private readonly IAnimalFactory _animalFactory;
    private readonly IMapper _mapper;

    public AnimalService(IAnimalQueryRepository animalQueryRepository,
        IAnimalCommandRepository animalCommandRepository,
        IAnimalFactory animalFactory,
        IMapper mapper)
    {
        _animalQueryRepository = animalQueryRepository;
        _animalCommandRepository = animalCommandRepository;
        _animalFactory = animalFactory;
        _mapper = mapper;
    }
    public AnimalViewModel? GetAnimal(Guid Id)
    {
        var animal = _animalQueryRepository.GetAnimal(Id);
        if (animal == null)
        {
            throw new NotFoundException("Animal", Id);
        }
        
        return animal;
    }

    public List<AnimalViewModel> GetAllAnimals()
    {
        return _animalQueryRepository.GetAllAnimals();
    }

    public Guid SaveAnimal(AnimalInputModel animalInputModel)
    {
        var animal = CreateAnimal(animalInputModel);

        return _animalCommandRepository.SaveAnimal(animal);
    }

    public void UpdateAnimal(Guid Id, AnimalInputModel animalInputModel)
    {
        _animalCommandRepository.UpdateAnimal(Id, CreateAnimal(animalInputModel));
    }

    public void DeleteAnimal(Guid Id)
    {
        _animalCommandRepository.DeleteAnimal(Id);
    }

    public CowViewModel? GetCow(Guid Id)
    {
        var cow = _animalQueryRepository.GetCow(Id);
        if (cow == null)
        {
            throw new NotFoundException("Cow", Id);
        }

        return cow;
    }

    public List<CowViewModel> GetAllCows()
    {
        return _animalQueryRepository.GetAllCows();
    }

    public Guid SaveCow(CowInputModel cowInputModel)
    {
        var cow = CreateCow(cowInputModel);

        return _animalCommandRepository.SaveCow(cow);
    }

    public void UpdateCow(Guid Id, CowInputModel cowInputModel)
    {
        _animalCommandRepository.UpdateCow(Id, CreateCow(cowInputModel));
    }

    public CalfViewModel? GetCalf(Guid Id)
    {
        var calf = _animalQueryRepository.GetCalf(Id);
        if (calf == null)
        {
            throw new NotFoundException("Calf", Id);
        }

        return calf;
    }

    public List<CalfViewModel> GetAllCalves()
    {
        return _animalQueryRepository.GetAllCalves();
    }

    public Guid SaveCalf(CalfInputModel calfInputModel)
    {
        VerifyMotherNumber(calfInputModel);

        var calf = CreateCalf(calfInputModel);
        return _animalCommandRepository.SaveCalf(calf);
    }

    public void UpdateCalf(Guid Id, CalfInputModel calfInputModel)
    {
        VerifyMotherNumber(calfInputModel);

        _animalCommandRepository.UpdateCalf(Id, CreateCalf(calfInputModel));
    }

    public BullViewModel? GetBull(Guid Id)
    {
        var bull = _animalQueryRepository.GetBull(Id);
        if (bull == null)
        {
            throw new NotFoundException("Bull", Id);
        }

        return bull;
    }

    public List<BullViewModel> GetAllBulls()
    {
        return _animalQueryRepository.GetAllBulls();
    }

    public Guid SaveBull(BullInputModel bullInputModel)
    {
        var bull = CreateBull(bullInputModel);

        return _animalCommandRepository.SaveBull(bull);
    }

    public void UpdateBull(Guid Id, BullInputModel bullInputModel)
    {
        _animalCommandRepository.UpdateBull(Id, CreateBull(bullInputModel));
    }

    private void VerifyMotherNumber(CalfInputModel calfInputModel)
    {
        if (!_animalQueryRepository.CowExists(calfInputModel.MotherNumber))
        {
            throw new KeyNotFoundException($"The cow with register number {calfInputModel.MotherNumber} does not exist.");
        }
    }

    private Animal CreateAnimal(AnimalInputModel animalInputModel)
    {
        return _animalFactory.Create(
            animalInputModel.Id ?? null,
            animalInputModel.RegisterNumber,
            new Arroba(animalInputModel.Weight),
            animalInputModel.Type,
            animalInputModel.Birthday);
    }

    private Cow CreateCow(CowInputModel cowInputModel)
    {
        return _animalFactory.Create(
            cowInputModel.Id ?? null,
            cowInputModel.RegisterNumber,
            new Arroba(cowInputModel.Weight),
            cowInputModel.Type,
            cowInputModel.Birthday,
            cowInputModel.IsPregnant,
            cowInputModel.HasCalf,
            cowInputModel.Name,
            cowInputModel.IsMilking);
    }

    private Calf CreateCalf(CalfInputModel calfInputModel)
    { 
        return _animalFactory.Create(
            calfInputModel.Id ?? null,
            calfInputModel.RegisterNumber,
            new Arroba(calfInputModel.Weight),
            calfInputModel.Type,
            calfInputModel.Birthday,
            calfInputModel.Gender,
            calfInputModel.MotherNumber);
    }

    private Bull CreateBull(BullInputModel bullInputModel)
    {
        return _animalFactory.Create(
            bullInputModel.Id ?? null,
            bullInputModel.RegisterNumber,
            new Arroba(bullInputModel.Weight),
            bullInputModel.Type,
            bullInputModel.Birthday,
            bullInputModel.Name ?? "");
    }
}
