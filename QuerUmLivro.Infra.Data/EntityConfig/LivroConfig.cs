using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuerUmLivro.Domain.Entities;

namespace QuerUmLivro.Infra.Data.EntityConfig
{
    public class LivroConfig : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("Livro");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnType("int")
                .UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("varchar(100)");
            builder.Property(p => p.Disponivel);
            builder.Property(p => p.DoadorId);

            builder.HasOne(i => i.Doador)
                .WithMany(l => l.Livros)
                .HasPrincipalKey(i => i.Id);

            builder.HasMany(l => l.Interesses)
                .WithOne(i => i.Livro)
                .HasForeignKey(l => l.LivroId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
