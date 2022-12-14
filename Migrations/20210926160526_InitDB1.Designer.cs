// <auto-generated />
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
    [Migration("20210926160526_InitDB1")]
    partial class InitDB1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookifyNew.Models.Buyout", b =>
                {
                    b.Property<int>("BuyoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BuyoutEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BuyoutQuauntity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BuyoutStartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("BuyoutTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("BuyoutId");

                    b.HasIndex("ProductId");

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

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("RentId")
                        .HasColumnType("int");

                    b.Property<string>("SellerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

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

                    b.Property<bool>("IsNew")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRentalAvailable")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("ProductImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductLongDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductShortDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrderId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BookifyNew.Models.Rental", b =>
                {
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId")
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

                    b.HasIndex("ProductId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("BookifyNew.Models.Buyout", b =>
                {
                    b.HasOne("BookifyNew.Models.Product", "Product")
                        .WithMany("Buyouts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BookifyNew.Models.Product", b =>
                {
                    b.HasOne("BookifyNew.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("BookifyNew.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.Navigation("Category");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BookifyNew.Models.Rental", b =>
                {
                    b.HasOne("BookifyNew.Models.Product", "Product")
                        .WithMany("Rentals")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BookifyNew.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("BookifyNew.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("BookifyNew.Models.Product", b =>
                {
                    b.Navigation("Buyouts");

                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
