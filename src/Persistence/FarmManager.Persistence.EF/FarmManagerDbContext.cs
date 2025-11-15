using FarmManager.Persistence.DataModels.Store;
using FarmManager.Persistence.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FarmManager.Persistence.EF.Context;

public class FarmManagerDbContext : DbContext
{
    public FarmManagerDbContext(DbContextOptions<FarmManagerDbContext> options) : base(options)
    {
    }

    public DbSet<AnimalDataModel> Animals { get; set; }
    public DbSet<CowDataModel> Cows { get; set; }
    public DbSet<BullDataModel> Bulls { get; set; }
    public DbSet<CalfDataModel> Calves { get; set; }
    public DbSet<LoteDataModel> Lotes { get; set; }
    public DbSet<ObservationDataModel> Observations { get; set; }
    public DbSet<ToqueDataModel> Toques { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AnimalConfiguration());
        modelBuilder.ApplyConfiguration(new CowConfiguration());
        modelBuilder.ApplyConfiguration(new BullConfiguration());
        modelBuilder.ApplyConfiguration(new CalfConfiguration());
        modelBuilder.ApplyConfiguration(new LoteConfiguration());
        modelBuilder.ApplyConfiguration(new ObservationConfiguration());
        modelBuilder.ApplyConfiguration(new ToqueConfiguration());
    }
}