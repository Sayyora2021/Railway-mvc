﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Railway.Infrastructure.Data;

#nullable disable

namespace Railway.Data.Migrations
{
    [DbContext(typeof(RailwayContext))]
    partial class RailwayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BuilletDestination", b =>
                {
                    b.Property<int>("BuilletsId")
                        .HasColumnType("int");

                    b.Property<int>("DestinationsId")
                        .HasColumnType("int");

                    b.HasKey("BuilletsId", "DestinationsId");

                    b.HasIndex("DestinationsId");

                    b.ToTable("BuilletDestination");
                });

            modelBuilder.Entity("BuilletTrain", b =>
                {
                    b.Property<int>("BuilletsId")
                        .HasColumnType("int");

                    b.Property<int>("TrainsId")
                        .HasColumnType("int");

                    b.HasKey("BuilletsId", "TrainsId");

                    b.HasIndex("TrainsId");

                    b.ToTable("BuilletTrain");
                });

            modelBuilder.Entity("Railway.Core.Entities.Buillet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Buillets");
                });

            modelBuilder.Entity("Railway.Core.Entities.Destination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Aller")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Retour")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("Railway.Core.Entities.Exemplaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BuilletId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("MiseEnService")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumeroInventaire")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BuilletId");

                    b.ToTable("Exemplaires");
                });

            modelBuilder.Entity("Railway.Core.Entities.Gare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gares");
                });

            modelBuilder.Entity("Railway.Core.Entities.Passager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("Cotisation")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Passagers");
                });

            modelBuilder.Entity("Railway.Core.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateAller")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModification")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateRetour")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExemplaireId")
                        .HasColumnType("int");

                    b.Property<int>("PassagerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExemplaireId");

                    b.HasIndex("PassagerId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Railway.Core.Entities.Train", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("BuilletDestination", b =>
                {
                    b.HasOne("Railway.Core.Entities.Buillet", null)
                        .WithMany()
                        .HasForeignKey("BuilletsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Railway.Core.Entities.Destination", null)
                        .WithMany()
                        .HasForeignKey("DestinationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BuilletTrain", b =>
                {
                    b.HasOne("Railway.Core.Entities.Buillet", null)
                        .WithMany()
                        .HasForeignKey("BuilletsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Railway.Core.Entities.Train", null)
                        .WithMany()
                        .HasForeignKey("TrainsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Railway.Core.Entities.Exemplaire", b =>
                {
                    b.HasOne("Railway.Core.Entities.Buillet", "Buillet")
                        .WithMany("Exemplaires")
                        .HasForeignKey("BuilletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buillet");
                });

            modelBuilder.Entity("Railway.Core.Entities.Reservation", b =>
                {
                    b.HasOne("Railway.Core.Entities.Exemplaire", "Exemplaire")
                        .WithMany()
                        .HasForeignKey("ExemplaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Railway.Core.Entities.Passager", "Passager")
                        .WithMany("ListReservation")
                        .HasForeignKey("PassagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exemplaire");

                    b.Navigation("Passager");
                });

            modelBuilder.Entity("Railway.Core.Entities.Buillet", b =>
                {
                    b.Navigation("Exemplaires");
                });

            modelBuilder.Entity("Railway.Core.Entities.Passager", b =>
                {
                    b.Navigation("ListReservation");
                });
#pragma warning restore 612, 618
        }
    }
}
