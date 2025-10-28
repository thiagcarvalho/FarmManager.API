using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Domain.Entities;

namespace FarmManager.Application.Services;

public class LoteService : ILoteService
{
    private readonly ILoteQueryRepository _loteQuerryRepository;
    private readonly ILoteCommandRepository _loteCommandRepository;
    private readonly IAnimalQueryRepository _animalQueryRepository;

    public LoteService(ILoteQueryRepository loteQuerryRepository,
        ILoteCommandRepository loteCommandRepository,
        IAnimalQueryRepository animalQueryRepository)
    {
        _loteQuerryRepository = loteQuerryRepository;
        _loteCommandRepository = loteCommandRepository;
        _animalQueryRepository = animalQueryRepository;
    }

    public void DeleteLote(int Id)
    {
        _loteCommandRepository.DeleteLote(Id);
    }

    public List<LoteViewModel> GetAllLotes()
    {
        return _loteQuerryRepository.GetAllLotes();
    }

    public int GetLoteIdByName(string name)
    {
        return _loteQuerryRepository.GetLoteIdByName(name);
    }

    public int SaveLote(LoteInputModel loteInputModel)
    {
        var lote = new Lote(null, loteInputModel.Name);

        var result = _loteCommandRepository.SaveLote(lote);

        return result;
    }

    public void UpdateLote(int Id, string lote)
    {
        throw new NotImplementedException();
    }

    public List<AnimalViewModel> GetAnimalsByLoteId(int loteId)
    {
        return _animalQueryRepository.GetAnimalsByLoteId(loteId);
    }
}
