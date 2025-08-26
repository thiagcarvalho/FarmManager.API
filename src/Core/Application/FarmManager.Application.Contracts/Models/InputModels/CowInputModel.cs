using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Application.Contracts.Models.InputModels;

public class CowInputModel : AnimalInputModel
{
    [Required(ErrorMessage = "You have to say if the Cow is pregnant or not")]
    public bool IsPregnant { get; set; }
    [Required(ErrorMessage = "You have to say if the Cow has a calf or not")]
    public bool HasCalf { get; set; }
    public string? Name { get; set; }
    [Required(ErrorMessage = "You have to say if the Cow is milking or not")]
    public bool IsMilking { get; set; }
}
