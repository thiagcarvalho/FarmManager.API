using FarmManager.Application.Contracts.Models.InputModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface IToqueCommandRepository
{
    int AddToque(ToqueInputModel toqueInputModelcs);
    void DeleteToque(int id);
}
