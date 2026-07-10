public class Order
{
    public int OrderId{get; set; }
    public ICollection<OrderItem> OrderItems { get; set;}= new List<OrderItem>();
    public DateTime OrderDate {get; set;} = DateTime.UtcNow;
    public  string Status {get ;set; } = null!;
    public  decimal TotalAmount{get; set;} 
    public int UserId {get; set;}
    public User User {get; set;} = null!;
}