namespace FarmManager.Application.Contracts.Models.ViewModels;

public class AnimalViewModel
{
    public Guid Id { get; set; }
    public int RegisterNumber { get; set; }
    public int Age { get; set; }
    public decimal Weight { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }

}
