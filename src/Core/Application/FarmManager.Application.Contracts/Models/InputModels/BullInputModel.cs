using System.ComponentModel.DataAnnotations;

namespace FarmManager.Application.Contracts.Models.InputModels;

public class BullInputModel : AnimalInputModel
{
    [Required(ErrorMessage = "You have to provide the Name of the Bull")]
    public string? Name { get; set; }
}
