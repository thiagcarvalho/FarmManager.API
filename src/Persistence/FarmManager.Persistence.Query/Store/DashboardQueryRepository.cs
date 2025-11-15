using FarmManager.Application.Contracts.Interfaces.Persistence.Queries;
using FarmManager.Application.Contracts.Models.ViewModels;
using FarmManager.Persistence.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FarmManager.Persistence.Query.Store;

public class DashboardQueryRepository : IDashboardQueryRepository
{
    private readonly FarmManagerDbContext _context;

    public DashboardQueryRepository(FarmManagerDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardViewModel> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow;

        var allAnimals = await _context.Animals
            .AsNoTracking()
            .Select(a => new { a.Id, a.RegisterNumber, a.Birthday, a.Type })
            .ToListAsync(cancellationToken);

        var cowsData = await _context.Cows
            .AsNoTracking()
            .Select(c => new { c.AnimalId, c.IsPregnant, c.IsMilking })
            .ToListAsync(cancellationToken);

        var calvesData = await _context.Calves
            .AsNoTracking()
            .Select(c => new { c.AnimalId, c.Gender })
            .ToListAsync(cancellationToken);

        var bullsData = await _context.Bulls
            .AsNoTracking()
            .Select(b => new { b.AnimalId })
            .ToListAsync(cancellationToken);

        var closeToCalvingCount = await _context.Toques
            .Where(t => t.vacaPrenha && t.dataPartoPrevisto >= today && t.dataPartoPrevisto <= today.AddDays(30))
            .Select(t => t.cowId)
            .Distinct()
            .CountAsync(cancellationToken);

        var calfGenderLookup = calvesData.ToDictionary(c => c.AnimalId, c => c.Gender);
        var bullsLookup = bullsData.Select(b => b.AnimalId).ToHashSet();

        var pregnantCount = cowsData.Count(c => c.IsPregnant);
        var milkingCount = cowsData.Count(c => c.IsMilking);
        var emptyCount = cowsData.Count(c => !c.IsPregnant);

        var animalEntries = allAnimals
            .Select(a => new
            {
                a.RegisterNumber,
                a.Birthday,
                IsFemale = a.Type == "Cow" || (a.Type == "Calf" && calfGenderLookup.TryGetValue(a.Id, out var gender) && gender)
            })
            .ToList();

        var ranges = new (string Label, int Min, int? Max)[]
        {
            ("Menor que 4 meses", 0, 3),
            ("De 4 a 8 meses", 4, 8),
            ("De 9 a 12 meses", 9, 12),
            ("De 13 a 24 meses", 13, 24),
            ("De 25 a 36 meses", 25, 36),
            ("Maior que 36 meses", 37, null)
        };

        var ageSummaryDtos = ranges.Select(r =>
        {
            var inRange = animalEntries
                .Where(a =>
                {
                    var ageMonths = (int)((today - a.Birthday).TotalDays / 30);
                    return (r.Max == null && ageMonths >= r.Min) || (ageMonths >= r.Min && ageMonths <= r.Max);
                });

            var femaleCount = inRange.Count(a => a.IsFemale);
            var maleCount = inRange.Count(a => !a.IsFemale);
            return new AgeSummaryViewModel { AgeRange = r.Label, Female = femaleCount, Male = maleCount };
        }).ToList();

        return new DashboardViewModel
        {
            PregnantCount = pregnantCount,
            MilkingCount = milkingCount,
            EmptyCount = emptyCount,
            CloseToCalvingCount = closeToCalvingCount,
            AgeSummary = ageSummaryDtos
        };
    }
}
