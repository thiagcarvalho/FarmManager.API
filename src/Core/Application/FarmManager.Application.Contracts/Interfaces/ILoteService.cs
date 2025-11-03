using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Domain.Entities;

namespace FarmManager.Application.Contracts.Interfaces;

public interface ILoteService
{
    int SaveLote(LoteInputModel loteInputModel);
    void UpdateLote(int Id, string lote);
    void DeleteLote(int Id);
    int GetLoteIdByName (string name);
    List<LoteViewModel> GetAllLotes();
    List<AnimalViewModel> GetAnimalsByLoteId(int loteId);
    List<LoteSummaryViewModel> GetLoteSummary();
}
