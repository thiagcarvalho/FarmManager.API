using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class CalfConfiguration : IEntityTypeConfiguration<CalfDataModel>
{
    public void Configure(EntityTypeBuilder<CalfDataModel> builder)
    {
        builder.ToTable("Calves");

        builder.HasKey(calf => calf.Id);
        builder.Property(calf => calf.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(calf => calf.Animal)
            .WithOne()
            .HasForeignKey<CalfDataModel>(calf => calf.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(calf => calf.Gender)
            .IsRequired();

        builder.Property(calf => calf.MotherNumber)
            .IsRequired();

        builder.Ignore(calf => calf.CreatedAt);
        builder.Ignore(calf => calf.CreatedBy);
        builder.Ignore(calf => calf.UpdatedAt);
        builder.Ignore(calf => calf.UpdatedBy);
    }
}
