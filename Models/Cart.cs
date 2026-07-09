public class Cart
{
    public int CartId { get; set;}
    public int UserId {get; set; }
    public User User {get; set;} = null!;
    public ICollection<CartItem> CartItems = new List<CartItem>();
}