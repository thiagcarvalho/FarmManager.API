using FarmManager.Persistence.DataModels.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FarmManager.Persistence.EF.Configurations;

public class ToqueConfiguration : IEntityTypeConfiguration<ToqueDataModel>
{
    public void Configure(EntityTypeBuilder<ToqueDataModel> builder)
    {
        builder.ToTable("Toques");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.HasOne(t => t.Cow)
               .WithMany()
               .HasForeignKey(t => t.cowId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();

        builder.Property(t => t.dataToque)
            .IsRequired()
            .HasColumnType("timestamp without time zone")
            .HasColumnName("dataToque");

        builder.Property(t => t.dataPartoPrevisto)
            .IsRequired()
            .HasColumnType("timestamp without time zone")
            .HasColumnName("dataPartoPrevisto");

        builder.Property(t => t.vacaPrenha);

        builder.Property(t => t.tempoGestacaoDias);

        builder.Property(t => t.observacoes)
            .IsRequired()
            .HasMaxLength(2000);

        builder.HasIndex(t => t.cowId);

    }
}
