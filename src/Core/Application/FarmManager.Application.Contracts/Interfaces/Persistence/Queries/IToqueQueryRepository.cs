using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface IToqueQueryRepository
{
    ToqueViewModel? GetToque(int id);
    List<ToqueViewModel> GetByAnimalId(Guid animalId);
    List<ToqueViewModel> GetAll();
}
