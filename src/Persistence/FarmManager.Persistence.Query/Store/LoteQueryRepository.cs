using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.EF.Context;

namespace FarmManager.Persistence.Query.Store;

public class LoteQueryRepository : ILoteQueryRepository
{
    private readonly FarmManagerDbContext _context;
    private readonly IMapper _mapper;

    public LoteQueryRepository(IMapper mapper, FarmManagerDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public List<LoteViewModel> GetAllLotes()
    {
        var lotes = _context
            .Lotes
            .ToList();

        return _mapper.Map<List<LoteViewModel>>(lotes);
    }

    public int GetLoteIdByName(string name)
    {
        var id = _context
            .Lotes
            .Where(l => l.Name == name)
            .Select(l => l.Id)
            .FirstOrDefault();

        return id;
    }
}
