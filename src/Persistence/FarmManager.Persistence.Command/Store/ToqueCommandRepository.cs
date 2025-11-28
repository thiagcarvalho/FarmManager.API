using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Application.Contracts.Models.InputModels;
using FarmManager.Persistence.DataModels.Store;
using FarmManager.Persistence.EF.Context;

namespace FarmManager.Persistence.Command.Store;

public class ToqueCommandRepository : IToqueCommandRepository
{
    private const int GESTACAO_TOTAL_DIAS = 280;
    private readonly IMapper _mapper;
    private readonly FarmManagerDbContext _context;

    public ToqueCommandRepository(IMapper mapper, FarmManagerDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public int AddToque(ToqueInputModel toqueInputModelcs)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var existingToque = GetToqueByCow(toqueInputModelcs.CowId);

            if (existingToque != null)
            {
                _context.Toques.Remove(existingToque);
            }

            var toqueDataModel = _mapper.Map<ToqueDataModel>(toqueInputModelcs);

            var cow = _context.Cows.FirstOrDefault(c => c.Id == toqueDataModel.cowId);

            if (cow == null)
            {
               throw new InvalidOperationException($"Vaca com Id {toqueDataModel.cowId} não encontrada.");
            }

            if (toqueDataModel.vacaPrenha)
            {
                toqueDataModel.dataPartoPrevisto = CalcularDataPartoPrevisto(toqueDataModel.dataToque, toqueDataModel.tempoGestacaoDias);
            }

            cow.IsPregnant = toqueDataModel.vacaPrenha;

            _context.Toques.Add(toqueDataModel);
            _context.SaveChanges();

            transaction.Commit();

            return toqueDataModel.Id;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DeleteToque(int id)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var toque = _context.Toques.FirstOrDefault(t => t.Id == id);

            if (toque != null)
            {
                var cow = _context.Cows.FirstOrDefault(c => c.Id == toque.cowId);

                if (cow != null)
                {
                    cow.IsPregnant = false;
                }

                _context.Toques.Remove(toque);
                _context.SaveChanges();

                transaction.Commit();
            }
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private ToqueDataModel? GetToqueByCow(int cowId)
    {
        return _context.Toques.FirstOrDefault(t => t.cowId == cowId);
    }

    private static DateTime CalcularDataPartoPrevisto(DateTime dataToque, int diasGestacaoAtual)
    {
        var diasRestantes = GESTACAO_TOTAL_DIAS - diasGestacaoAtual;
        return dataToque.AddDays(diasRestantes);
    }
}
