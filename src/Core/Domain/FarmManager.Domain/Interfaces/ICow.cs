using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Domain.Interfaces;

public interface ICow : IAnimal
{
    bool IsPregnant { get; set; }
    bool HasCalf { get; set; }
    string? Name { get; set; }
    bool IsMilking { get; set; }
}
