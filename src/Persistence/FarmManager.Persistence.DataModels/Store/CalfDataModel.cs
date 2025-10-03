namespace FarmManager.Persistence.DataModels.Store;

public class CalfDataModel : DataModelBase
{
    public int Id { get; set; }
    public Guid AnimalId { get; set; }
    public bool Gender { get; set; }
    public int MotherNumber { get; set; }

    public AnimalDataModel Animal { get; set; } = null!;
}
