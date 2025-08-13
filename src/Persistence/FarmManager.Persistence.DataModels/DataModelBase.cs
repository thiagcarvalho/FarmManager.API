namespace FarmManager.Persistence.DataModels;

public abstract class DataModelBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
