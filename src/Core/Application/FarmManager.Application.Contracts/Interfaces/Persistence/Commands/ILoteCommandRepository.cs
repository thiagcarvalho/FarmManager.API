namespace FarmManager.Application.Contracts.Interfaces.Persistence.Commands;

public interface ILoteCommandRepository
{
    Guid SaveLote(string loteName);
    void UpdateLote(int Id, string lote);
    void DeleteLote(int Id);
}
