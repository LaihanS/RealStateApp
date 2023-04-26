﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealStateApp.Infrastructure.Persistence.Contexts;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230417133702_Mejoras")]
    partial class Mejoras
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.Mejora", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("modifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Mejoras", (string)null);
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.PropertyImages", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("IdPropiedad")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("modifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("IdPropiedad");

                    b.ToTable("Imagenes", (string)null);
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.PropertyType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("modifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("PropertyTypes", (string)null);
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.Propiedades", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("AgenteId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AgenteNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MtsTerrain")
                        .HasColumnType("int");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<int>("PropertyTypeId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityBaños")
                        .HasColumnType("int");

                    b.Property<int>("QuantityHabitaciones")
                        .HasColumnType("int");

                    b.Property<int>("TipoVenta")
                        .HasColumnType("int");

                    b.Property<string>("UnicDigitSequence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VentaTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("modifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("PropertyTypeId");

                    b.HasIndex("VentaTypeId");

                    b.ToTable("Propiedades", (string)null);
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.PropiedadMejora", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("MejoraId")
                        .HasColumnType("int");

                    b.Property<int?>("PropiedadId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("modifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("MejoraId");

                    b.HasIndex("PropiedadId");

                    b.ToTable("PropiedadMejoras", (string)null);
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.VentaType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("created")
                        .HasColumnType("datetime2");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("modifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("VentaTypes", (string)null);
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.PropertyImages", b =>
                {
                    b.HasOne("RealStateApp.Core.Domain.Entities.Propiedades", "Propiedad")
                        .WithMany("Imagenes")
                        .HasForeignKey("IdPropiedad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Propiedad");
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.Propiedades", b =>
                {
                    b.HasOne("RealStateApp.Core.Domain.Entities.PropertyType", "PropiedadType")
                        .WithMany("Propiedades")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RealStateApp.Core.Domain.Entities.VentaType", "VentaType")
                        .WithMany("Propiedades")
                        .HasForeignKey("VentaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropiedadType");

                    b.Navigation("VentaType");
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.PropiedadMejora", b =>
                {
                    b.HasOne("RealStateApp.Core.Domain.Entities.Mejora", "Mejora")
                        .WithMany("PropiedadMejoras")
                        .HasForeignKey("MejoraId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RealStateApp.Core.Domain.Entities.Propiedades", "Propiedad")
                        .WithMany("PropiedadMejoras")
                        .HasForeignKey("PropiedadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mejora");

                    b.Navigation("Propiedad");
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.Mejora", b =>
                {
                    b.Navigation("PropiedadMejoras");
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.PropertyType", b =>
                {
                    b.Navigation("Propiedades");
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.Propiedades", b =>
                {
                    b.Navigation("Imagenes");

                    b.Navigation("PropiedadMejoras");
                });

            modelBuilder.Entity("RealStateApp.Core.Domain.Entities.VentaType", b =>
                {
                    b.Navigation("Propiedades");
                });
#pragma warning restore 612, 618
        }
    }
}
