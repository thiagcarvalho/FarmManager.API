using AutoMapper;
using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using FarmManager.Domain.Entities;
using FarmManager.Domain.Interfaces;
using FarmManager.Domain.Interfaces.Factories;
using FarmManager.Domain.ValueObject;
using System;

namespace FarmManager.Application.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalQueryRepository _animalQueryRepository;
    private readonly IAnimalCommandRepository _animalCommandRepository;
    private readonly ILoteService _loteService;
    private readonly IObservationService _observationService;
    private readonly IAnimalFactory _animalFactory;
    private readonly IMapper _mapper;

    public AnimalService(IAnimalQueryRepository animalQueryRepository,
        IAnimalCommandRepository animalCommandRepository,
        ILoteService loteService,
        IObservationService observationService,
        IAnimalFactory animalFactory,
        IMapper mapper)
    {
        _animalQueryRepository = animalQueryRepository;
        _animalCommandRepository = animalCommandRepository;
        _loteService = loteService;
        _observationService = observationService;
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
        VerifyAnimalRegisterNumber(animalInputModel.RegisterNumber);

        var animal = CreateAnimal(animalInputModel);

        var animalId = _animalCommandRepository.SaveAnimal(animal);

        return animalId;
    }

    public void UpdateAnimal(Guid Id, AnimalInputModel animalInputModel)
    {
        VerifyAnimalExistsByType(animalInputModel.RegisterNumber, "Animal");
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

    public CowViewModel? GetCowByRegisterNumber(int registerNumber)
    {
        return _animalQueryRepository.GetCowByRegisterNumber(registerNumber);
    }

    public Guid SaveCow(CowInputModel cowInputModel)
    {
        VerifyAnimalRegisterNumber(cowInputModel.RegisterNumber);

        var cow = CreateCow(cowInputModel);

        var cowlId = _animalCommandRepository.SaveCow(cow);

        if (cowInputModel.Obs != null && cowInputModel.Obs.Count != 0)
        {
            _observationService.AddObservation(cowlId, cowInputModel.Obs);
        }

        return cowlId;
    }

    public void UpdateCow(Guid Id, CowInputModel cowInputModel)
    {
        VerifyAnimalRegisterNumberForUpdate(cowInputModel.RegisterNumber, Id);
        _animalCommandRepository.UpdateCow(Id, CreateCow(cowInputModel));

        _observationService.UpdateObservation(Id, cowInputModel.Obs);
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

        var calflId = _animalCommandRepository.SaveCalf(calf);

        if (calfInputModel.Obs != null && calfInputModel.Obs.Count != 0)
        {
            _observationService.AddObservation(calflId, calfInputModel.Obs);
        }

        return calflId;
    }

    public void UpdateCalf(Guid Id, CalfInputModel calfInputModel)
    {
        VerifyMotherNumber(calfInputModel);

        _animalCommandRepository.UpdateCalf(Id, CreateCalf(calfInputModel));
        _observationService.UpdateObservation(Id, calfInputModel.Obs);
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
        VerifyAnimalRegisterNumber(bullInputModel.RegisterNumber);

        var bull = CreateBull(bullInputModel);

        var bullId = _animalCommandRepository.SaveBull(bull);

        if (bullInputModel.Obs != null && bullInputModel.Obs.Count != 0)
        {
            _observationService.AddObservation(bullId, bullInputModel.Obs);
        }

        return bullId;
    }

    public CalfViewModel? GetCalfByMotherNumber(int motherNumber)
    {
        return _animalQueryRepository.GetCalfByMotherNumber(motherNumber);
    }

    public void UpdateBull(Guid Id, BullInputModel bullInputModel)
    {
        VerifyAnimalRegisterNumberForUpdate(bullInputModel.RegisterNumber, Id);
        _animalCommandRepository.UpdateBull(Id, CreateBull(bullInputModel));
        _observationService.UpdateObservation(Id, bullInputModel.Obs);
    }

    private void VerifyAnimalRegisterNumber(int registerNumber)
    {
        if (_animalQueryRepository.AnimalExistsByRegisterNumber(registerNumber))
        {
            throw new DuplicateResourceException("Animal", registerNumber);
        }
    }

    private void VerifyAnimalExistsByType(int registerNumber, string entity)
    {
        if (!_animalQueryRepository.AnimalExistsByRegisterNumber(registerNumber))
        {
            throw new NotFoundException($"The {entity} with register number {registerNumber} does not exist.");
        }
    }

    private void VerifyAnimalRegisterNumberForUpdate(int registerNumber, Guid animalId)
    {
        if (_animalQueryRepository.AnimalExistsByRegisterNumberExcludingId(registerNumber, animalId))
        {
            throw new DuplicateResourceException("Animal", registerNumber);
        }
    }

    private void VerifyMotherNumber(CalfInputModel calfInputModel)
    {
        if (!_animalQueryRepository.AnimalExistsByRegisterNumberAndType(calfInputModel.MotherNumber, "Cow"))
        {
            throw new NotFoundException($"A vaca com o numero de registro {calfInputModel.MotherNumber} não existe.");
        }
    }

    private Animal CreateAnimal(AnimalInputModel animalInputModel)
    {
        return _animalFactory.Create(
            animalInputModel.Id ?? null,
            animalInputModel.RegisterNumber,
            new Arroba(animalInputModel.Weight),
            animalInputModel.Type,
            animalInputModel.Birthday,
            _loteService.GetLoteIdByName(animalInputModel.Lote));
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
            cowInputModel.IsMilking,
            _loteService.GetLoteIdByName(cowInputModel.Lote));
    }

    private Calf CreateCalf(CalfInputModel calfInputModel)
    { 
        return _animalFactory.Create(
            calfInputModel.Id ?? null,
            calfInputModel.MotherNumber,
            new Arroba(calfInputModel.Weight),
            calfInputModel.Type,
            calfInputModel.Birthday,
            calfInputModel.Gender,
            calfInputModel.MotherNumber,
            _loteService.GetLoteIdByName(calfInputModel.Lote));
    }

    private Bull CreateBull(BullInputModel bullInputModel)
    {
        return _animalFactory.Create(
            bullInputModel.Id ?? null,
            bullInputModel.RegisterNumber,
            new Arroba(bullInputModel.Weight),
            bullInputModel.Type,
            bullInputModel.Birthday,
            bullInputModel.Name ?? "",
            _loteService.GetLoteIdByName(bullInputModel.Lote));
    }
}
