using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Domain.Entities;
using FarmManager.Persistence.DataModels.Store;
using FarmManager.Persistence.EF.Context;

namespace FarmManager.Persistence.Command.Store;

public class LoteCommandRepository : ILoteCommandRepository
{
    private readonly IMapper _mapper;
    private readonly FarmManagerDbContext _context;

    public LoteCommandRepository(IMapper mapper, FarmManagerDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public int SaveLote(Lote lote)
    {
        var loteDataModel = _mapper.Map<LoteDataModel>(lote);

        _context.Lotes.Add(loteDataModel);
        _context.SaveChanges();

        return 0;
    }

    public void UpdateLote(int Id, string lote)
    {
        throw new NotImplementedException();
    }

    public void DeleteLote(int Id)
    {
        var lote = _context.Lotes.Find(Id);
        if (lote != null)
        {
            _context.Lotes.Remove(lote);
            _context.SaveChanges();
        }
    }
}
