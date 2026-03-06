using FinancialControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialControl.Infrastructure.Persistence.Configurations;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Descricao)
            .IsRequired()
            .HasMaxLength(400);

        builder.OwnsOne(t => t.Valor, v =>
        {
            v.Property(p => p.Quantia)
                .HasColumnName("Valor")
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        });

        builder.Property(t => t.Tipo)
            .IsRequired();
    }
}
