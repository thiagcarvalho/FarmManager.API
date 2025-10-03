using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class CowConfiguration : IEntityTypeConfiguration<CowDataModel>
{
    public void Configure(EntityTypeBuilder<CowDataModel> builder)
    {
        builder.ToTable("Cows");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(c => c.Animal)
            .WithOne()
            .HasForeignKey<CowDataModel>(c => c.AnimalId)
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

        builder.Ignore(c => c.CreatedAt);
        builder.Ignore(c => c.CreatedBy);
        builder.Ignore(c => c.UpdatedAt);
        builder.Ignore(c => c.UpdatedBy);
    }
}
