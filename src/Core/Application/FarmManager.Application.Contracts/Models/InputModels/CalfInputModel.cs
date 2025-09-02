using System.ComponentModel.DataAnnotations;

namespace FarmManager.Application.Contracts.Models.InputModels;

public class CalfInputModel : AnimalInputModel
{
    [Required(ErrorMessage = "You have to say if the Calf is male or female")]
    public bool Gender { get; set; }
    [Required(ErrorMessage = "You have to provide the Mother Number of the Calf")]
    public int MotherNumber { get; set; }
}
