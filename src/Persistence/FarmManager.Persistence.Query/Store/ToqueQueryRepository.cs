using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Persistence.Query.Store;

public class ToqueQueryRepository : IToqueQueryRepository
{
    private readonly FarmManagerDbContext _context;
    private readonly IMapper _mapper;

    public ToqueQueryRepository(FarmManagerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    List<ToqueViewModel> IToqueQueryRepository.GetAll()
    {
        var toques = _context.Toques
            .AsNoTracking()
            .Include(t => t.Cow)
                .ThenInclude(c => c.Animal)
            .ToList();

        return _mapper.Map<List<ToqueViewModel>>(toques);
    }

    List<ToqueViewModel> IToqueQueryRepository.GetByAnimalId(int cowId)
    {
        var toques = _context.Toques
            .AsNoTracking()
            .Include(t => t.Cow)
                .ThenInclude(c => c.Animal)
            .Where(t => t.cowId == cowId)
            .ToList();

        return _mapper.Map<List<ToqueViewModel>>(toques);
    }

    ToqueViewModel? IToqueQueryRepository.GetToque(int id)
    {
        var toque = _context.Toques
            .FirstOrDefault(t => t.Id == id);

        return toque == null ? null : _mapper.Map<ToqueViewModel>(toque);
    }
}
