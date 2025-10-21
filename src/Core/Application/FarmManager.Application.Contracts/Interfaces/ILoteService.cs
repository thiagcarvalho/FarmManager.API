using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces;

public interface ILoteService
{
    Guid SaveLote(string lote);
    void DeleteLote(int Id);
    LoteViewModel? GetLote(string Id);
    List<LoteViewModel> GetAllLotes();
}
