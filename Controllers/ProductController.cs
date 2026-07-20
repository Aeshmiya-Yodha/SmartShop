using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("Product")]
public class ProductController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult> GetProductByID ([FromRoute] int id)
    {
         await Task.Delay(500);
         return Ok("Success");
    }
    
    [HttpGet]
    public async Task<ActionResult> GetProduct([FromQuery] string ProductCategory ,  [FromQuery] string ProductName )
    {
       await Task.Delay(500);
       return Ok("Success!");
    }
}