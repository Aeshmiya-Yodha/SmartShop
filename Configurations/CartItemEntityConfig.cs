using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CartItemConfig :IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(C=>C.CartItemId);
        builder.Property(C=> C.CartId).IsRequired();
        builder.Property(C=>C.ProductId).IsRequired();
        builder.Property(C =>C.Quantity).IsRequired();

    }
}