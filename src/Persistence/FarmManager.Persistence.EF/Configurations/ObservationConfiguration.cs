using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class ObservationConfiguration : IEntityTypeConfiguration<ObservationDataModel>
{
    public void Configure(EntityTypeBuilder<ObservationDataModel> builder)
    {
        builder.ToTable("Observations");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        builder.Property(o => o.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(o => o.DataAdd)
            .IsRequired()
            .HasColumnType("timestamp without time zone")
            .HasColumnName("DataAdd");

        builder.HasIndex(o => o.AnimalId);

        builder.HasOne(o => o.Animal)
               .WithMany()
               .HasForeignKey(o => o.AnimalId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
}