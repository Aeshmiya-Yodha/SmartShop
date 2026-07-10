using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure (EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(A=>A.AddressId);
        builder.Property(A => A.AddressLane).IsRequired();
        builder.Property(A => A.City).IsRequired().HasMaxLength(50);
        builder.Property(A=>A.State).IsRequired().HasMaxLength(50);
        builder.Property(A=>A.Country).IsRequired().HasMaxLength(50);
        builder.Property(A=> A.PostalCode).IsRequired().HasMaxLength(50);
                
    }
}