using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(100);
        builder.Property(p => p.Category).HasMaxLength(20);
        builder.Property(p => p.Image).HasMaxLength(255);
    }
}
