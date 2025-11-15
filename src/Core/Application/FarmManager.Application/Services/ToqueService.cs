using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Application.Exceptions;
using System.Reflection.Metadata.Ecma335;

namespace FarmManager.Application.Services;

public class ToqueService : IToqueService
{
    private readonly IToqueQueryRepository _toqueQueryRepository;
    private readonly IToqueCommandRepository _toqueCommandRepository;
    private readonly IAnimalQueryRepository _animalQueryRepository; 
    public ToqueService(IToqueQueryRepository toqueQueryRepository,
        IToqueCommandRepository toqueCommandRepository,
        IAnimalQueryRepository animalQueryRepository)
    {
        _toqueQueryRepository = toqueQueryRepository;
        _toqueCommandRepository = toqueCommandRepository;
        _animalQueryRepository = animalQueryRepository;
    }
    
    public int AddToque(ToqueInputModel toqueInputModel)
    {
        var cowId = _animalQueryRepository.GetCowIdByRegisterNumber(toqueInputModel.registerNumber);

        if (cowId == null)
        {
            throw new NotFoundException($"The cow with register number {toqueInputModel.registerNumber} does not exist.");
        }

        toqueInputModel.CowId = cowId.Value;
        return _toqueCommandRepository.AddToque(toqueInputModel);
    }

    public int DeleteExpiredToques()
    {
        var allToques = _toqueQueryRepository.GetAll();
        var today = DateTime.Today;

        var expiredToques = allToques
            .Where(t => t.DataPartoPrevisto < today)
            .ToList();

        foreach (var toque in expiredToques)
        {
            _toqueCommandRepository.DeleteToque(toque.Id);
        }

        return expiredToques.Count;
    }

    public void DeleteToque(int id)
    {
        _toqueCommandRepository.DeleteToque(id);
    }

    public List<ToqueViewModel> GetAll()
    {
        return _toqueQueryRepository.GetAll();
    }

    public List<ToqueViewModel> GetByAnimalId(Guid animalID)
    {
        var cowId = _animalQueryRepository.GetCowIdByAnimalId(animalID);

        if (cowId == null)
        {
            throw new NotFoundException($"The cow with register number {animalID} does not exist.");
        }

        return _toqueQueryRepository.GetByAnimalId(cowId.Value);
    }

    public ToqueViewModel? GetToque(int id)
    {
        return _toqueQueryRepository.GetToque(id);
    }
}
