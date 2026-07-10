public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}

public class Order
{
    public int OrderId{get; set; }
    public ICollection<OrderItem> OrderItems { get; set;}= new List<OrderItem>();
    public DateTime OrderDate {get; set;} = DateTime.UtcNow;
    public  OrderStatus Status {get ;set; } = OrderStatus.Pending;
    public bool isPaid {get; set;} = false;
    public  decimal TotalAmount{get; set;} 
    public int UserId {get; set;}
    public User User {get; set;} = null!;
}