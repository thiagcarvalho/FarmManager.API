using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface ILoteQueryRepository
{
    int GetLoteIdByName (string name);
    string? GetLoteById(int id);
    List<LoteViewModel> GetAllLotes();
}
