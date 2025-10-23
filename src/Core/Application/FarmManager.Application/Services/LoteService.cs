using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Domain.Entities;

namespace FarmManager.Application.Services;

public class LoteService : ILoteService
{

    private readonly ILoteQuerryRepository _loteQuerryRepository;
    private readonly ILoteCommandRepository _loteCommandRepository;

    public LoteService(ILoteQuerryRepository loteQuerryRepository,
        ILoteCommandRepository loteCommandRepository)
    {
        _loteQuerryRepository = loteQuerryRepository;
        _loteCommandRepository = loteCommandRepository;
    }

    public void DeleteLote(int Id)
    {
        throw new NotImplementedException();
    }

    public List<LoteViewModel> GetAllLotes()
    {
        throw new NotImplementedException();
    }

    public LoteViewModel? GetLote(string Id)
    {
        throw new NotImplementedException();
    }

    public LoteViewModel? GetLoteByName(string name)
    {
        throw new NotImplementedException();
    }

    public int SaveLote(Lote lote)
    {
        throw new NotImplementedException();
    }

    public void UpdateLote(int Id, string lote)
    {
        throw new NotImplementedException();
    }
}
