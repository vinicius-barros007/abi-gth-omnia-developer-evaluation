using Ambev.DeveloperEvaluation.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Identity;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UserId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(70);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        builder.OwnsOne(p => p.Address, a =>
        {
            a.WithOwner();

            a.Property(a => a.City)
                .HasColumnName("City")
                .IsRequired()
                .HasMaxLength(50);

            a.Property(a => a.Street)
                .HasColumnName("Street")
                .IsRequired()
                .HasMaxLength(100);

            a.Property(a => a.Number)
                .HasColumnName("Number")
                .IsRequired();

            a.Property(a => a.ZipCode)
                .HasColumnName("ZipCode")
                .IsRequired()
                .HasMaxLength(10);

            a.OwnsOne(a => a.GeoLocation, g =>
            {
                g.WithOwner();

                g.Property(g => g.Latitude)
                    .HasColumnName("Latitude")
                    .IsRequired();

                g.Property(g => g.Longitude)
                    .HasColumnName("Longitude")
                    .IsRequired();
            });
        });
    }
}
