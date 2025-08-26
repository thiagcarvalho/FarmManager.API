using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Application.Contracts.Models.ViewModels;

public class CowViewModel : AnimalViewModel
{
    public string? Name { get; set; }
    public bool IsPregnant { get; set; }
    public bool HasCalf { get; set; }
    public bool IsMilking { get; set; }
}
