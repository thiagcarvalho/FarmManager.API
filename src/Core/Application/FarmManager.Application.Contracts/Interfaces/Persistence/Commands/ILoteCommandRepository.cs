using FarmManager.Domain.Entities;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface ILoteCommandRepository
{
    int SaveLote(Lote lote);
    void UpdateLote(int Id, string lote);
    void DeleteLote(int Id);
}
