using System.ComponentModel.DataAnnotations;

namespace FarmManager.Application.Contracts.Models.InputModels;

public class LoteInputModel
{
    [Required(ErrorMessage = "The Lote Name is required")]
    [MaxLength(100, ErrorMessage = "The Lote Name must be less than 100 characters")]
    public string Name { get; set; } = string.Empty;
}
