using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces;

public interface IToqueService
{
    int AddToque(ToqueInputModel toqueInputModelcs);
    void DeleteToque(int id);
    int DeleteExpiredToques();
    ToqueViewModel? GetToque(int id);
    List<ToqueViewModel> GetByAnimalId(Guid animalID);
    List<ToqueViewModel> GetAll();
}
