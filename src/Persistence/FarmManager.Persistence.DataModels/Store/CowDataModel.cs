using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Persistence.DataModels.Store;

public class CowDataModel : DataModelBase
{
    public int Id { get; set; }
    public Guid AnimalId { get; set; }
    public bool IsMilking { get; set; }
    public bool IsPregnant { get; set; }
    public bool HasCalf { get; set; }
    public string? Name { get; set; }
    public AnimalDataModel Animal { get; set; } = null!;
}
