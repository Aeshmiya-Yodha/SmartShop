using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<List<Product>>> GetProductByID([FromRoute] int id)
    {
        List<Product> ProductsById = await _context.Products.Where(p => p.ProductId == id).ToListAsync();
        return Ok(ProductsById);
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProduct([FromQuery] string ProductCategory, [FromQuery] string ProductName)
    {
        //needs the fix we need to make filtering 
        IQueryable<Product> AllProducts = _context.Products;
        List<Product> Products = new List<Product>();
        if (string.IsNullOrEmpty(ProductCategory))
        {
            if (string.IsNullOrEmpty(ProductName))
            {
                Products =await  AllProducts.ToListAsync();
            }
            else
            {
                Products =await AllProducts.Where(p => p.ProductName == ProductName).ToListAsync();
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

                Products = await AllProducts.Where(product => product.ProductCategoryId == categoryId).ToListAsync();
            }
            else
            {
                //Join Operations on Product Category 
                int categoryId = _context.ProductCategories
                    .Where(category => category.ProductCategoryName == ProductCategory)
                    .Select(category => category.ProductCategoryId)
                    .FirstOrDefault();

                Products =await AllProducts.Where(product => product.ProductCategoryId == categoryId && product.ProductName == ProductName).ToListAsync();
            }
        }
        return Ok(Products);
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] Product Product)
    {
        var ExistingProduct = await _context.Products.Where(P => P.ProductId == Product.ProductId).FirstOrDefaultAsync();
        if (ExistingProduct != null)
        {
            return BadRequest("Product Already Exist");
        }
        else
        {
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            return Ok("Product Added successfully!");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product UpdatedProduct)
    {
        var ExistingProduct = await _context.Products.Where(P => P.ProductId == id).FirstOrDefaultAsync();
        if (ExistingProduct == null)
        {
            return BadRequest("Product does not exist");
        }
        else
        {
            //Update the Existing Product with the values 
            return Ok("Product updated successfully");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute] int id)
    {
        var ExistingProduct = await _context.Products.Where(P => P.ProductId == id).FirstOrDefaultAsync();
        if (ExistingProduct == null)
        {
            return BadRequest("Product does not exist");
        }

        _context.Products.Remove(ExistingProduct);
        await _context.SaveChangesAsync();
        return Ok("Product deleted successfully!");
    }
}