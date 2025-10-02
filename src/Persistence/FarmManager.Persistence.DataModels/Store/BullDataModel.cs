namespace FarmManager.Persistence.DataModels.Store;

public class BullDataModel : DataModelBase
{
    public int Id { get; set; }
    public Guid AnimalId { get; set; }
    public string? Name { get; set; }

    public AnimalDataModel Animal { get; set; } = null!;
}
