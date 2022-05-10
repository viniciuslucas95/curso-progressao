﻿// <auto-generated />
using System;
using CursoProgressao.Api.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CursoProgressao.Api.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CursoProgressao.Api.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CellPhone")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Landline")
                        .HasColumnType("text");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Documents.ResponsibleDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ResponsibleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Rg")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ResponsibleId")
                        .IsUnique();

                    b.ToTable("ResponsibleDocuments");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Documents.StudentDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cpf")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Rg")
                        .HasColumnType("text");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentDocuments");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Residence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Residences");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Responsible", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Responsibles");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Contact", b =>
                {
                    b.HasOne("CursoProgressao.Api.Models.Student", "Student")
                        .WithOne("Contact")
                        .HasForeignKey("CursoProgressao.Api.Models.Contact", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Documents.ResponsibleDocument", b =>
                {
                    b.HasOne("CursoProgressao.Api.Models.Responsible", "Responsible")
                        .WithOne("Document")
                        .HasForeignKey("CursoProgressao.Api.Models.Documents.ResponsibleDocument", "ResponsibleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Responsible");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Documents.StudentDocument", b =>
                {
                    b.HasOne("CursoProgressao.Api.Models.Student", "Student")
                        .WithOne("Document")
                        .HasForeignKey("CursoProgressao.Api.Models.Documents.StudentDocument", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Residence", b =>
                {
                    b.HasOne("CursoProgressao.Api.Models.Student", "Student")
                        .WithOne("Residence")
                        .HasForeignKey("CursoProgressao.Api.Models.Residence", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Responsible", b =>
                {
                    b.HasOne("CursoProgressao.Api.Models.Student", "Student")
                        .WithOne("Responsible")
                        .HasForeignKey("CursoProgressao.Api.Models.Responsible", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Responsible", b =>
                {
                    b.Navigation("Document")
                        .IsRequired();
                });

            modelBuilder.Entity("CursoProgressao.Api.Models.Student", b =>
                {
                    b.Navigation("Contact")
                        .IsRequired();

                    b.Navigation("Document")
                        .IsRequired();

                    b.Navigation("Residence")
                        .IsRequired();

                    b.Navigation("Responsible")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
