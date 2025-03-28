﻿// <auto-generated />
using System;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    [DbContext(typeof(DefaultContext))]
    partial class DefaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Identity.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Identity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Products.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Products.ProductRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductRating", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sales.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("SaleNumber")
                        .HasColumnType("bigserial");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Sale", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sales.SaleItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<decimal>("Discount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("SaleId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleItem", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Identity.Person", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Identity.User", "User")
                        .WithOne("Person")
                        .HasForeignKey("Ambev.DeveloperEvaluation.Domain.Entities.Identity.Person", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("City");

                            b1.Property<int>("Number")
                                .HasColumnType("integer")
                                .HasColumnName("Number");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("Street");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("ZipCode")
                                .HasAnnotation("Relational:JsonPropertyName", "zipcode");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");

                            b1.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.GeoLocation", "GeoLocation", b2 =>
                                {
                                    b2.Property<Guid>("AddressPersonId")
                                        .HasColumnType("uuid");

                                    b2.Property<decimal>("Latitude")
                                        .HasColumnType("decimal(10,7)")
                                        .HasColumnName("Latitude")
                                        .HasAnnotation("Relational:JsonPropertyName", "lat");

                                    b2.Property<decimal>("Longitude")
                                        .HasColumnType("decimal(10,7)")
                                        .HasColumnName("Longitude")
                                        .HasAnnotation("Relational:JsonPropertyName", "long");

                                    b2.HasKey("AddressPersonId");

                                    b2.ToTable("Person");

                                    b2.HasAnnotation("Relational:JsonPropertyName", "geolocation");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressPersonId");
                                });

                            b1.Navigation("GeoLocation")
                                .IsRequired();
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Products.Product", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Products.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Products.ProductRating", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Products.Product", "Product")
                        .WithOne("Rating")
                        .HasForeignKey("Ambev.DeveloperEvaluation.Domain.Entities.Products.ProductRating", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sales.Sale", b =>
                {
                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.Branch", "Branch", b1 =>
                        {
                            b1.Property<Guid>("SaleId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("BranchId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(70)
                                .HasColumnType("character varying(70)")
                                .HasColumnName("BranchName");

                            b1.HasKey("SaleId");

                            b1.ToTable("Sale");

                            b1.WithOwner()
                                .HasForeignKey("SaleId");
                        });

                    b.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.Customer", "Customer", b1 =>
                        {
                            b1.Property<Guid>("SaleId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("CustomerId");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(180)
                                .HasColumnType("character varying(180)")
                                .HasColumnName("CustomerName");

                            b1.HasKey("SaleId");

                            b1.ToTable("Sale");

                            b1.WithOwner()
                                .HasForeignKey("SaleId");
                        });

                    b.Navigation("Branch")
                        .IsRequired();

                    b.Navigation("Customer")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sales.SaleItem", b =>
                {
                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ambev.DeveloperEvaluation.Domain.Entities.Sales.Sale", "Sale")
                        .WithMany("Items")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Identity.User", b =>
                {
                    b.Navigation("Person")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Products.Product", b =>
                {
                    b.Navigation("Rating")
                        .IsRequired();
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Products.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Sales.Sale", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
