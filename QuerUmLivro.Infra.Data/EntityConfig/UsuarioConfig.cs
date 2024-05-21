using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuerUmLivro.Domain.Entities;
using QuerUmLivro.Domain.Entities.ValueObjects;

namespace QuerUmLivro.Infra.Data.EntityConfig
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnType("int")
                .UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("varchar(100)");
            builder.Property(p => p.Email).HasColumnType("varchar(100)");

            builder.Property(u => u.Email)
             .IsRequired()
             .HasMaxLength(100)
             .HasConversion(
                 email => email.EnderecoEmail, 
                 value => new Email(value) 
             );

            builder.HasMany(l => l.Interesses)
                .WithOne(i => i.Interessado)
                .HasForeignKey(l => l.InteressadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.Livros)
                .WithOne(i => i.Doador)
                .HasForeignKey(l => l.DoadorId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
