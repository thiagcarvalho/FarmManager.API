using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class CowConfiguration : IEntityTypeConfiguration<CowDataModel>
{
    public void Configure(EntityTypeBuilder<CowDataModel> builder)
    {
        builder.ToTable("Cows");

        builder.HasOne<AnimalDataModel>()
            .WithOne()
            .HasForeignKey<CowDataModel>(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(c => c.IsMilking)
            .IsRequired();

        builder.Property(c => c.IsPregnant)
            .IsRequired();

        builder.Property(c => c.HasCalf)
            .IsRequired();
    }
}
