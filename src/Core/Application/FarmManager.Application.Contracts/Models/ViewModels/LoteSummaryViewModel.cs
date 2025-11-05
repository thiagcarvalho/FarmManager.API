namespace FarmManager.Application.Contracts.Models.ViewModels;

public class LoteSummaryViewModel
{
    public int LoteId { get; set; }
    public string LoteName { get; set; } = string.Empty;
    public int TotalAnimals { get; set; }
    public int Cows { get; set; }
    public int Calves { get; set; }
    public int Bulls { get; set; }
}