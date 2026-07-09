public class Product
{
    public  int ProductId{get; set; }
    public required string ProductName {get; set;}
    public string ?  ProductDescription {get; set;}   
    public  decimal Price {get; set;}
    public int StockQuantity {get; set;}
    public int ProductCategoryId {get; set;}
    public bool IsActive {get; set; }
    public string ? ImageUrl { get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public  ProductCategory  ProductCategory {get; set;} = null! ;
    public ICollection<CartItem> CartItems {get;set;} = new List<CartItem>();

}