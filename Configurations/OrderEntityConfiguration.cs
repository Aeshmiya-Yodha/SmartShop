using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(O => O.OrderId);
        builder.Property(O => O.OrderDate).IsRequired();
        builder.Property(O => O.Status).IsRequired();
        builder.Property(O => O.TotalAmount).IsRequired();
        builder.Property(O => O.UserId).IsRequired();
    }
}