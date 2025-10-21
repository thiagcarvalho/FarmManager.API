using FarmManager.Domain.Interfaces;
using FarmManager.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Domain.Entities;

public class Cow : Animal, ICow
{
    public bool IsPregnant { get; set; }
    public bool HasCalf { get; set; }
    public string? Name { get; set; }
    public bool IsMilking { get; set; }

    public Cow(Guid? id, 
        int registerNumber, 
        Arroba weight, 
        string type, 
        DateTime birthday,
        string name,
        bool isPregnant,
        bool hasCalf,
        bool isMilking,
        string loteName) : base(id, registerNumber, weight, type, birthday, loteName)
    {
        Name = name;
        IsPregnant = isPregnant;
        HasCalf = hasCalf;
        IsMilking = isMilking;
    }

}
