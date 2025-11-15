using System.ComponentModel.DataAnnotations;

namespace FarmManager.Application.Contracts.Models.InputModels;

public class ToqueInputModel
{
    public int registerNumber { get; set; }
    public int CowId { get; set; }

    [Required(ErrorMessage = "Data Prenha is required")]
    public DateTime DataToque { get; set; }

    [Required(ErrorMessage = "Vaca Prenha is required")]
    public bool VacaPrenha { get; set; }

    public int TempoGestacaoDias { get; set; }

    [MaxLength(100, ErrorMessage = "The observation cannot exceed 100 characters")]
    public string Observacoes { get; set; } = string.Empty;
}
