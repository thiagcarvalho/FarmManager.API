using System.ComponentModel.DataAnnotations;

namespace FarmManager.Application.Contracts.Models.InputModels;

public class AnimalInputModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "The Register Number is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The Register Number must be greater than 0")]
    public int RegisterNumber { get; set; }

    [Required(ErrorMessage = "The Birthday is required")]
    public DateTime Birthday { get; set; }

    [Required(ErrorMessage = "The Weight is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "The Weight must be greater than 0")]
    public decimal Weight { get; set; }

    [Required(ErrorMessage = "The Type is required")]
    public string Type { get; set; } = string.Empty;
}
