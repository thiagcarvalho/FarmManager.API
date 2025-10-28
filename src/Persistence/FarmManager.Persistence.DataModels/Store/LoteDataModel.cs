using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Persistence.DataModels.Store;

public class LoteDataModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<AnimalDataModel> Animals { get; set; } = new List<AnimalDataModel>();
}
