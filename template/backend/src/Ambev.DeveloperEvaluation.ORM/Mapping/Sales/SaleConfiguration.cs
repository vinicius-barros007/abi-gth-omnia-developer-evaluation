using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Sales;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.SaleNumber)
            .IsRequired() 
            .HasDefaultValueSql("nextval('sale_seq')")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.SaleDate)
            .IsRequired();

        builder.OwnsOne(s => s.Branch, b =>
        {
            b.WithOwner();

            b.Property(x => x.Id)
                .HasColumnName("BranchId")
                .HasColumnType("uuid")
                .IsRequired();

            b.Property(x => x.Name)
                .HasColumnName("BranchName")
                .IsRequired()
                .HasMaxLength(70);
        });

        builder.OwnsOne(s => s.Customer, c =>
        {
            c.WithOwner();

            c.Property(x => x.Id)
                .HasColumnName("CustomerId")
                .HasColumnType("uuid")
                .IsRequired();

            c.Property(x => x.Name)
                .HasColumnName("CustomerName")
                .IsRequired()
                .HasMaxLength(180);
        });

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt);

        builder.HasMany(s => s.Items)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .HasPrincipalKey(s => s.Id);
    }
}
