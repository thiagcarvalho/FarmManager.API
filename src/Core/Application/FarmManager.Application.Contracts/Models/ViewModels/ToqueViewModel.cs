using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManager.Application.Contracts.Models.ViewModels;

public class ToqueViewModel
{
    public int Id { get; set; }
    public DateTime DataToque { get; set; }
    public bool VacaPrenha { get; set; }
    public int TempoGestacaoDias { get; set; }
    public string Observacoes { get; set; } = string.Empty;
    public DateTime DataPartoPrevisto { get; set; }
}
