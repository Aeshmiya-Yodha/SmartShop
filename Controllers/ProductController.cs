using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("Product")]
public class ProductController : ControllerBase
{

    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<List<Product>>> GetProductByID([FromRoute] int id, [FromQuery] string ProductCategory, [FromQuery] string ProductName)
    {
        IEnumerable<Product> ProductsById = _context.Products.Where(p => p.ProductId == id);
        List<Product> Products = new List<Product>();
        if (string.IsNullOrEmpty(ProductCategory))
        {
            if (string.IsNullOrEmpty(ProductName))
            {
                Products = ProductsById.ToList();
            }
            else
            {
                Products = ProductsById.Where(p => p.ProductName == ProductName).ToList();
            }
        }
        else
        {
            if (string.IsNullOrEmpty(ProductName))
            {
            
                int categoryId = _context.ProductCategories
                    .Where(category => category.ProductCategoryName == ProductCategory)
                    .Select(category => category.ProductCategoryId)
                    .FirstOrDefault();

                Products = ProductsById.Where(product => product.ProductCategoryId == categoryId).ToList();
            }
            else
            {
                //Join Operations on Product Category 
                int categoryId = _context.ProductCategories
                    .Where(category => category.ProductCategoryName == ProductCategory)
                    .Select(category => category.ProductCategoryId)
                    .FirstOrDefault();

                Products = ProductsById.Where(product => product.ProductCategoryId == categoryId  && product.ProductName == ProductName).ToList();
            }
        }
        return Ok(Products);
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProduct([FromQuery] string ProductCategory, [FromQuery] string ProductName)
    {
        IEnumerable<Product> AllProducts = _context.Products.ToList();
        
        List<Product> products = new List<Product>();
        return products; 

    }
}