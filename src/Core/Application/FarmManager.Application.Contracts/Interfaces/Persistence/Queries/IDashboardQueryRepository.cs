using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Contracts.Interfaces.Persistence.Queries;

public interface IDashboardQueryRepository
{
    Task<DashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default);
}
