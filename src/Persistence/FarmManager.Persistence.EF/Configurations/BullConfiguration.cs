using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class BullConfiguration : IEntityTypeConfiguration<BullDataModel>
{
    public void Configure(EntityTypeBuilder<BullDataModel> builder)
    {
        builder.HasOne<AnimalDataModel>()
            .WithOne()
            .HasForeignKey<BullDataModel>(b => b.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}
