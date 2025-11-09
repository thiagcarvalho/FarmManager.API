using FarmManager.Application.Contracts.Interfaces;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Application.Contracts.Models.ViewModels;

namespace FarmManager.Application.Services;

public class ToqueService : IToqueService
{
    private readonly IToqueQueryRepository _toqueQueryRepository;
    private readonly IToqueCommandRepository _toqueCommandRepository;
    public ToqueService(IToqueQueryRepository toqueQueryRepository,
        IToqueCommandRepository toqueCommandRepository)
    {
        _toqueQueryRepository = toqueQueryRepository;
        _toqueCommandRepository = toqueCommandRepository;
    }

    public int AddToque(ToqueInputModel toqueInputModel)
    {
        return _toqueCommandRepository.AddToque(toqueInputModel);
    }

    public int DeleteExpiredToques()
    {
        var allToques = _toqueQueryRepository.GetAll();
        var today = DateTime.Today;

        var expiredToques = allToques
            .Where(t => t.DataPartoPrevisto < today)
            .ToList();

        foreach (var toque in expiredToques)
        {
            _toqueCommandRepository.DeleteToque(toque.Id);
        }

        return expiredToques.Count;
    }

    public void DeleteToque(int id)
    {
        _toqueCommandRepository.DeleteToque(id);
    }

    public List<ToqueViewModel> GetAll()
    {
        return _toqueQueryRepository.GetAll();
    }

    public List<ToqueViewModel> GetByAnimalId(int cowId)
    {
        return _toqueQueryRepository.GetByAnimalId(cowId);
    }

    public ToqueViewModel? GetToque(int id)
    {
        return _toqueQueryRepository.GetToque(id);
    }
}
