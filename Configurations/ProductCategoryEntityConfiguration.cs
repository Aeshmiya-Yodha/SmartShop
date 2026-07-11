using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(P => P.ProductCategoryId);
        builder.Property(P => P.ProductCategoryName).IsRequired().HasMaxLength(50);
        builder.Property(P => P.IsActive).IsRequired();
    }
}