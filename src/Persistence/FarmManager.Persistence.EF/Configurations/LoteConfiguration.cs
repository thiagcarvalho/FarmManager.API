using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class LoteConfiguration : IEntityTypeConfiguration<LoteDataModel>
{
    public void Configure(EntityTypeBuilder<LoteDataModel> builder)
    {
        builder.ToTable("Lotes");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd();

        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(l => l.Name)
            .IsUnique();

        builder.HasMany(l => l.Animals)
               .WithOne(a => a.Lote)
               .HasForeignKey(a => a.LoteId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}