using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces;

public interface IToqueService
{
    void AddToque(ToqueInputModelcs toqueInputModelcs);
    void DeleteToque(int id);
    ToqueViewModel? GetToque(int id);
    List<ToqueViewModel> GetByAnimalId(Guid animalId);
    List<ToqueViewModel> GetAll();
}
