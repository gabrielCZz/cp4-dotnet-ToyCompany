using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToyCompany.Domain.Entities;

namespace ToyCompany.Infrastructure.Persistence.Configurations;

public sealed class BrinquedoConfiguration : IEntityTypeConfiguration<Brinquedo>
{
    public void Configure(EntityTypeBuilder<Brinquedo> builder)
    {
        builder.ToTable("TDS_TB_BRINQUEDOS");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasColumnName("ID_BRINQUEDO")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.NomeBrinquedo)
            .HasColumnName("NOME_BRINQUEDO")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.TipoBrinquedo)
            .HasColumnName("TIPO_BRINQUEDO")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(b => b.Classificacao)
            .HasColumnName("CLASSIFICACAO")
            .IsRequired();

        builder.Property(b => b.Tamanho)
            .HasColumnName("TAMANHO")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(b => b.Preco)
            .HasColumnName("PRECO")
            .HasPrecision(10, 2)
            .IsRequired();
    }
}
