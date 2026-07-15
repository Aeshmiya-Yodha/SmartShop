using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("Product")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public ActionResult GetProduct([FromQuery] string ProductCategory , [FromQuery] string ProductName )
    {
       return Ok("Success!");
    }

    [HttpGet("{id}")]
    public ActionResult GetProductbyId([FromRoute] int id )
    {
        return Ok("Success");
    }
}