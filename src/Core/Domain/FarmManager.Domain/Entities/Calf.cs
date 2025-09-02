using FarmManager.Domain.Interfaces;
using FarmManager.Domain.ValueObject;

namespace FarmManager.Domain.Entities;

public class Calf : Animal, ICalf
{
    public bool Gender { get; set; }
    public int MotherNumber { get; set; }
    internal Calf(Guid? id, 
        int registerNumber, 
        Arroba weight, 
        string type, 
        DateTime birthday,
        bool gender,
        int motherNumber) : base(id, registerNumber, weight, type, birthday)
    {
        Gender = gender;
        MotherNumber = motherNumber;
    }

}
