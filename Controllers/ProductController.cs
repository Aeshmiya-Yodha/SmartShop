using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("Product")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public ActionResult GetProduct([FromQuery] string ProductCategory , [FromQuery] int id , [FromQuery] string ProductName )
    {
       return Ok("Success!");
    }
}