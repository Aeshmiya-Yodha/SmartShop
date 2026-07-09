public class CartItem
{
    public int CartItemId{get; set;}
    public int CartId {get; set;}
    public int ProductId {get; set;}
    public int Quantity {get; set; }
    public Product Product { get ;set; }= null!;
    public Cart Cart {get; set;} = null!;

}