using System.ComponentModel.DataAnnotations;

namespace FarmManager.Application.Contracts.Models.InputModels;

public class ToqueInputModelcs
{

    [MaxLength(ErrorMessage = "Data Prenha is required")]
    public DateTime DataToque { get; set; }

    [MaxLength(ErrorMessage = "Vaca Prenha is required")]
    public bool VacaPrenha { get; set; }

    [MaxLength(ErrorMessage = "Tempo de Gestacao is required")]
    public int TempoGestacaoDias { get; set; }

    [MaxLength(100, ErrorMessage = "The observation cannot exceed 100 characters")]
    public string Observacoes { get; set; } = string.Empty;
}
