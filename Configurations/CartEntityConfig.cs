using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(C => C.CartId);
        builder.Property(C => C.UserId).IsRequired();
        builder.HasOne(U => U.User).WithOne(C => C.Cart).HasForeignKey<Cart>(C => C.UserId);
    }
}