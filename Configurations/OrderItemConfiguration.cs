using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure (EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(O => O.OrderItemId);
        builder.Property(O => O.OrderId).IsRequired();
        builder.Property(O => O.ProductId).IsRequired();
        builder.Property(O => O.PriceWhenOrdered).IsRequired();
        builder.Property( O => O.Quantity).IsRequired();
    }
}