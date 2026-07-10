public class ProductCategory
{
    public int ProductCategoryId {get; set;}
    public required string ProductCategoryName {get; set;}
    public bool IsActive {get; set;} = true;
    public string ? ProductDescription {get; set; }
    public string ? ImageUrl {get; set;}
    public ICollection<Product> Products {get; set; }= new List<Product>();
}