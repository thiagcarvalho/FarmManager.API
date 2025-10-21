using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Domain.Entities;

namespace FarmManager.Application.Contracts.Interfaces;

public interface ILoteService
{
    Guid SaveLote(Lote lote);
    void DeleteLote(int Id);
    LoteViewModel? GetLote(string Id);
    List<LoteViewModel> GetAllLotes();
}
