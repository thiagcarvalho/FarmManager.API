namespace FarmManager.Application.Contracts.Models.ViewModels;

public class DashboardViewModel
{
    public int PregnantCount { get; set; }
    public int EmptyCount { get; set; }
    public int CloseToCalvingCount { get; set; }
    public int MilkingCount { get; set; }
    public List<AgeSummaryViewModel> AgeSummary { get; set; } = new();
}
