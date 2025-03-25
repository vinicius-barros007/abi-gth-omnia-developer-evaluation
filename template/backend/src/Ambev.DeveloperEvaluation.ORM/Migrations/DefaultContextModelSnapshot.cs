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

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Person", b =>
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

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.User", b =>
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

            modelBuilder.Entity("Ambev.DeveloperEvaluation.Domain.Entities.Person", b =>
                {
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
                                .HasColumnName("ZipCode");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");

                            b1.OwnsOne("Ambev.DeveloperEvaluation.Domain.ValueObjects.GeoLocation", "GeoLocation", b2 =>
                                {
                                    b2.Property<Guid>("AddressPersonId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Latitude")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)")
                                        .HasColumnName("Latitude");

                                    b2.Property<string>("Longitude")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("character varying(20)")
                                        .HasColumnName("Longitude");

                                    b2.HasKey("AddressPersonId");

                                    b2.ToTable("Person");

                                    b2.WithOwner()
                                        .HasForeignKey("AddressPersonId");
                                });

                            b1.Navigation("GeoLocation")
                                .IsRequired();
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
