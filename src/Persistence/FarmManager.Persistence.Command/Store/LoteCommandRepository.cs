using AutoMapper;
using FarmManager.Application.Contracts.Interfaces.Persistence.Commands;
using FarmManager.Domain.Entities;
using FarmManager.Domain.Interfaces;
using FarmManager.Persistence.DataModels.Store;
using FarmManager.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public void DeleteLote(int Id)
    {
        throw new NotImplementedException();
    }

    public int SaveLote(Lote lote)
    {

        var loteDataModel = new LoteDataModel
        {
            Name = lote.Name
        };

        _context.Lotes.Add(loteDataModel);
        _context.SaveChanges();

        return 0;
    }

    public void UpdateLote(int Id, string lote)
    {
        throw new NotImplementedException();
    }
}
