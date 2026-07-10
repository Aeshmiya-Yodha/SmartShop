using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);
        builder.Property(p => p.ProductName).IsRequired().HasMaxLength(150);
        builder.Property(p => p.ProductDescription).HasMaxLength(1000);
        builder.Property(p => p.Price).HasPrecision(18, 2);   // money precision
        builder.HasOne(p => p.ProductCategory)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.ProductCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}