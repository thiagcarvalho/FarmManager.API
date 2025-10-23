using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Domain.Entities;

namespace FarmManager.Application.Contracts.Interfaces;

public interface ILoteService
{
    int SaveLote(Lote lote);
    void UpdateLote(int Id, string lote);
    void DeleteLote(int Id);
    LoteViewModel? GetLote(string Id);
    LoteViewModel? GetLoteByName (string name);
    List<LoteViewModel> GetAllLotes();
}
