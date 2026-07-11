using Microsoft.EntityFrameworkCore;

public class AppCofig : DbContext
{
    DbSet<Address> Addresses {get; set;}
    DbSet<Cart> Carts {get; set;}
    DbSet<CartItem> CartItems {get; set;}
    DbSet<Order> Orders {get; set;}
    DbSet<OrderItem> OrderItems {get; set;}
    DbSet<Product> Products{get; set;}
    DbSet<ProductCategory> ProductCategories{get; set;}
    DbSet<User> Users { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppCofig).Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql();//add configuration or use the dependancy injection 
    }


}