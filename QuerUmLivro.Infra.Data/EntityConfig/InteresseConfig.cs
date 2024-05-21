using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;


namespace QuerUmLivro.Infra.Data.EntityConfig
{
    internal class InteresseConfig : IEntityTypeConfiguration<Interesse>
    {
        public void Configure(EntityTypeBuilder<Interesse> builder)
        {
            builder.ToTable("Interesse");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnType("int")
                .UseIdentityColumn();
            builder.Property(p => p.Justificativa)
                .IsRequired()
                .HasColumnType("varchar(100)");
            builder.Property(p => p.InteressadoId).IsRequired();
            builder.Property(p => p.LivroId).IsRequired();            
            builder.Property(p => p.Data).IsRequired();

            builder.Property(p => p.Status)
            .HasConversion(
                v => v.Value,    // Converte StatusInteresse para int
                v => StatusInteresse.From((StatusInteresse.StatusInteresseEnum)v))  // Converte int para StatusInteresse
            .IsRequired();

            builder.HasOne(i => i.Livro)
                .WithMany(l => l.Interesses)
                .HasPrincipalKey(i => i.Id);

            builder.HasOne(i => i.Interessado)
                .WithMany(l => l.Interesses)
                .HasPrincipalKey(i => i.Id);

          


        }
    }
}
