﻿// <auto-generated />
using System;
using BookifyNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BookifyNew.Migrations
{
    [DbContext(typeof(BFDBContext))]
    [Migration("20211009175117_DBChangeAgain")]
    partial class DBChangeAgain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookifyNew.Models.BuyRentHistory", b =>
                {
                    b.Property<int>("OrdeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BuyerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BuyoutTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsBought")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRented")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RentEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RentStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SellerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrdeId");

                    b.ToTable("Buyouts");
                });

            modelBuilder.Entity("BookifyNew.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookifyNew.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BuyerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BuyoutTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsBought")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRented")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RentEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RentStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SellerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookifyNew.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRentalAvailable")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("ProductLongDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductShortDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BookifyNew.Models.Rental", b =>
                {
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RentEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RentQuauntity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RentStartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("RentTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("RentId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("BookifyNew.Models.Product", b =>
                {
                    b.HasOne("BookifyNew.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BookifyNew.Models.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
