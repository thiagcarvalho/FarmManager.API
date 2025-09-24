using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class CalfConfiguration : IEntityTypeConfiguration<CalfDataModel>
{
    public void Configure(EntityTypeBuilder<CalfDataModel> builder)
    {
        builder.HasOne<AnimalDataModel>()
            .WithOne()
            .HasForeignKey<CalfDataModel>(calf => calf.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(calf => calf.Gender)
            .IsRequired();

        builder.Property(calf => calf.MotherNumber)
            .IsRequired();
    }
}
