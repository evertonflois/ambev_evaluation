using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
{
    public void Configure(EntityTypeBuilder<ProductRating> builder)
    {
        builder.ToTable("ProductRating");

        builder.HasKey(u => u.ProductId);
        builder.Property(u => u.ProductId);

        builder.Property(u => u.Rate).IsRequired();
        builder.Property(u => u.Count).IsRequired();        
    }
}
