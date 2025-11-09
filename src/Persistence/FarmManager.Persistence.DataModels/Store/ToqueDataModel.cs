namespace FarmManager.Persistence.DataModels.Store;

public class ToqueDataModel
{
    public int Id { get; set; }
    public int cowId { get; set; }
    public DateTime dataToque{ get; set; }
    public bool vacaPrenha { get; set; }
    public int tempoGestacaoDias { get; set; }
    public string observacoes { get; set; } = null!;

    public CowDataModel Cow { get; set; } = null!;
}
