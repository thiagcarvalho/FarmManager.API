using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class BullConfiguration : IEntityTypeConfiguration<BullDataModel>
{
    public void Configure(EntityTypeBuilder<BullDataModel> builder)
    {
        builder.ToTable("Bulls");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(b => b.Animal)
            .WithOne()
            .HasForeignKey<BullDataModel>(b => b.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(b => b.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Ignore(b => b.CreatedAt);
        builder.Ignore(b => b.CreatedBy);
        builder.Ignore(b => b.UpdatedAt);
        builder.Ignore(b => b.UpdatedBy);
    }
}
