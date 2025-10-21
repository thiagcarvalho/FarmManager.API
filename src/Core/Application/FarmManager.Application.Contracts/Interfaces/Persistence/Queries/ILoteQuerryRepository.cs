using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface ILoteQuerryRepository
{
    LoteViewModel? GetLote(string Id);
    List<LoteViewModel> GetAllLotes();
}
