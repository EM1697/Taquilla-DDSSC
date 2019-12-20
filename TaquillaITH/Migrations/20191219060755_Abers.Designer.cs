﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaquillaITH.Data;

namespace TaquillaITH.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191219060755_Abers")]
    partial class Abers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaquillaITH.Models.DaySales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("NomalTicketsAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("NomalTicketsCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Tickets3DAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Tickets3DCount")
                        .HasColumnType("int");

                    b.Property<decimal>("Tickets3DVIPAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TicketsVIPCount")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("DaySales");
                });

            modelBuilder.Entity("TaquillaITH.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Fecha_final")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Fecha_inicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Num_Sala")
                        .HasColumnType("int");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RunningTime")
                        .HasColumnType("int");

                    b.Property<string>("Schedule")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Synopsis")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("TaquillaITH.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cash")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreditCard")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RewardPoints")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("TaquillaITH.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DaySalesId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("MovieName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoBoletoId")
                        .HasColumnType("int");

                    b.Property<int>("TotalAmount")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DaySalesId");

                    b.HasIndex("MovieId");

                    b.HasIndex("PaymentId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("TaquillaITH.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Occupied")
                        .HasColumnType("bit");

                    b.Property<int?>("TheatreRoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheatreRoomId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("TaquillaITH.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TheatreRoomId")
                        .HasColumnType("int");

                    b.Property<string>("UsedSeats")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheatreRoomId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("TaquillaITH.Models.TheatreRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TheatreRooms");
                });

            modelBuilder.Entity("TaquillaITH.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Asiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BoletoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Hora")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombrePelicula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Sala")
                        .HasColumnType("int");

                    b.Property<int?>("SaleId")
                        .HasColumnType("int");

                    b.Property<int>("TipoBoletoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TaquillaITH.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReceiverFourDigits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderFourDigits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TaquillaITH.Models.Sale", b =>
                {
                    b.HasOne("TaquillaITH.Models.DaySales", "DaySales")
                        .WithMany()
                        .HasForeignKey("DaySalesId");

                    b.HasOne("TaquillaITH.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("TaquillaITH.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");
                });

            modelBuilder.Entity("TaquillaITH.Models.Seat", b =>
                {
                    b.HasOne("TaquillaITH.Models.TheatreRoom", null)
                        .WithMany("Seats")
                        .HasForeignKey("TheatreRoomId");
                });

            modelBuilder.Entity("TaquillaITH.Models.Show", b =>
                {
                    b.HasOne("TaquillaITH.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaquillaITH.Models.TheatreRoom", "TheatreRoom")
                        .WithMany()
                        .HasForeignKey("TheatreRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaquillaITH.Models.Ticket", b =>
                {
                    b.HasOne("TaquillaITH.Models.Sale", null)
                        .WithMany("Products")
                        .HasForeignKey("SaleId");
                });
#pragma warning restore 612, 618
        }
    }
}