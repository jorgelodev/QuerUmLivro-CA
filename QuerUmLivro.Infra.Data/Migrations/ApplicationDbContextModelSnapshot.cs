﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuerUmLivro.Domain.Entities.ValueObjects;
using QuerUmLivro.Infra.Data.Context;

#nullable disable

namespace QuerUmLivro.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuerUmLivro.Domain.Entities.Interesse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aprovado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("InteressadoId")
                        .HasColumnType("int");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<StatusInteresse.StatusInteresseEnum>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InteressadoId");

                    b.HasIndex("LivroId");

                    b.ToTable("Interesse", (string)null);
                });

            modelBuilder.Entity("QuerUmLivro.Domain.Entities.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<int>("DoadorId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DoadorId");

                    b.ToTable("Livro", (string)null);
                });

            modelBuilder.Entity("QuerUmLivro.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Desativado")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("QuerUmLivro.Domain.Entities.Interesse", b =>
                {
                    b.HasOne("QuerUmLivro.Domain.Entities.Usuario", "Interessado")
                        .WithMany("Interesses")
                        .HasForeignKey("InteressadoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QuerUmLivro.Domain.Entities.Livro", "Livro")
                        .WithMany("Interesses")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Interessado");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("QuerUmLivro.Domain.Entities.Livro", b =>
                {
                    b.HasOne("QuerUmLivro.Domain.Entities.Usuario", "Doador")
                        .WithMany("Livros")
                        .HasForeignKey("DoadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Doador");
                });

            modelBuilder.Entity("QuerUmLivro.Domain.Entities.Livro", b =>
                {
                    b.Navigation("Interesses");
                });

            modelBuilder.Entity("QuerUmLivro.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("Interesses");

                    b.Navigation("Livros");
                });
#pragma warning restore 612, 618
        }
    }
}
