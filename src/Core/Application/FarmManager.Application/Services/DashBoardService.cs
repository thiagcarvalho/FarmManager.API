using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Services;

public class DashBoardService : IDashBoardService
{
    private readonly IDashboardQueryRepository _dashboardQueryRepository;

    public DashBoardService(IDashboardQueryRepository dashboardQueryRepository)
    {
        _dashboardQueryRepository = dashboardQueryRepository;
    }

    public async Task<DashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        return await _dashboardQueryRepository.GetDashboardAsync(cancellationToken);
    }
}
