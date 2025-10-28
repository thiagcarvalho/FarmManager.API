using FarmManager.Domain.Interfaces;

namespace FarmManager.Domain.Entities;

public class Lote : ILote
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Lote(int? id, string name)
    {
        Id = id;
        Name = name;
    }
}
