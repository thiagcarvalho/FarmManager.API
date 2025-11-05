namespace FarmManager.Persistence.DataModels.Store;

public class ObservationDataModel
{
    public int Id { get; set; }
    public Guid AnimalId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DataAdd { get; set; }

    public AnimalDataModel Animal { get; set; } = null!;
}