namespace FarmManager.Persistence.DataModels.Store;

public class AnimalDataModel : DataModelBase
{
    public int RegisterNumber { get; set; }
    public int Age { get; set; }
    public decimal Weight { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public int? LoteId { get; set; }
    public LoteDataModel Lote { get; set; } = null!;
}
