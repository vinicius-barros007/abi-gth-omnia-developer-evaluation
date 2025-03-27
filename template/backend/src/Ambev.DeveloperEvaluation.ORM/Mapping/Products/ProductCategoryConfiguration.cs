using Ambev.DeveloperEvaluation.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Products;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategory");
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(pc => pc.Description)
            .IsRequired()
            .HasMaxLength(70);

        builder.Property(pc => pc.CreatedAt)
            .IsRequired();

        builder.Property(pc => pc.UpdatedAt);

        builder.HasMany(pc => pc.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .HasPrincipalKey(pc => pc.Id);
    }
}