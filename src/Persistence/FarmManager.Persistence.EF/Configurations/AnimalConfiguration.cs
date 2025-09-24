using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class AnimalConfiguration : IEntityTypeConfiguration<AnimalDataModel>
{
    public void Configure(EntityTypeBuilder<AnimalDataModel> builder)
    {
        builder.ToTable("Animals");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .IsRequired();

        builder.Property(a => a.RegisterNumber)
            .IsRequired();

        builder.Property(a => a.Age)
            .IsRequired();

        builder.Property(a => a.Weight)
            .IsRequired();

        builder.Property(a => a.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Birthday)
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.CreatedBy)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.UpdatedAt)
            .IsRequired(false);

        builder.Property(a => a.UpdatedBy)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.HasDiscriminator<string>("AnimalType")
            .HasValue<AnimalDataModel>("Animal")
            .HasValue<CowDataModel>("Cow")
            .HasValue<CalfDataModel>("Calf");
    }
}
