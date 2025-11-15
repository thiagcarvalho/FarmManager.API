namespace FarmManager.Application.Contracts.Models.ViewModels;
public class AgeSummaryViewModel
{
    public string AgeRange { get; set; } = string.Empty;
    public int Female { get; set; }
    public int Male { get; set; }
    public int Total => Female + Male;
}
