using FarmManager.Application.Contracts.Models.InputModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface IToqueCommandRepository
{
    void AddToque(ToqueInputModelcs toqueInputModelcs);
    void DeleteToque(int id);
}
